import * as Koa from 'koa';
import  appConfig from './config';
import fetch from 'node-fetch';
import * as jwt from 'jsonwebtoken';
import * as winston from 'winston';

interface UserClaims {
    sub: string;  // subject - the Facebook user id
    iss: string;  // issuer - https://texas-msa.org
    permissions: string;
}

export interface UserState {
    user?: UserClaims;
}

export class UnauthenticatedUserError implements Error {
    name: string = "UnauthenticatedUserError";
    message: string = "No authenticated user";
    stack: string;
}

export class UnauthorizedUserError implements Error {
    name: string = "UnauthorizedUserError";
    message: string = "User is unauthorized";
    stack: string;
}

/**
 * Verifies that a Facebook access token is valid, and if so, gets the associated Facebook userId 
 * @param accessToken The token to validate
 * @returns Facebook user id iff the token is valid, null otherwise
 */
export async function verifyFacebookAccessToken(accessToken: string) : Promise<string>
{
    const fbConfig = appConfig.api.facebook;
    const appToken = `${ fbConfig.appID }|${ fbConfig.appSecret }`;

    const url = `https://graph.facebook.com/v2.8/debug_token?access_token=${ appToken }&input_token=${ accessToken }`
    const response = await fetch(url);
    const body = await response.json();

    const data = body['data'];
    if (!data || data.error || !data.is_valid) return null;

    return data.user_id;
}

export function generateJSONWebToken(username: string, permissions: string) : string
{
    const claim : UserClaims = { iss: "http://texas-msa.org", sub: username, permissions: permissions};
    return jwt.sign(claim, appConfig.api.secret);
}

/**
 * Create a Koa middleware that can authenticate an HTTP request
 * by decrypting a JSON Web Token included in the header
 * 
 * It expects the token to be included as the 'authorization' header
 * and prefixed by 'Bearer '
 * @param secret The secret to use for decrypting the JWTs
 */
export function authenticator(secret: string) : Koa.Middleware {
    return async function(ctx, next) {
        const authorizationHeader: string = ctx.header['authorization'];
        if (authorizationHeader) {
            const [, token] = authorizationHeader.split(' ');
            try {
                const decodedToken = jwt.verify(token, secret);
                const userState: UserState = ctx.state as UserState;
                userState.user = decodedToken as UserClaims;
            } catch (err) {
                winston.warn(`Invalid JWT token supplied`);
            }
        }

        try {
            await next();
        }
        catch (e) {
            if (e instanceof UnauthenticatedUserError) {
                ctx.response.status = 401;
            }
            else if (e instanceof UnauthorizedUserError) {
                ctx.response.status = 403;
            }
        }
    }
} 