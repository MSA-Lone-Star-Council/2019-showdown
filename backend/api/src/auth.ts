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

export async function verifyFacebookAccessToken(accessToken: string) : Promise<boolean>
{
    const fbConfig = appConfig.api.facebook;
    const appToken = `${ fbConfig.appID }|${ fbConfig.appSecret }`;

    const url = `https://graph.facebook.com/v2.8/debug_token?access_token=${ appToken }&input_token=${ accessToken }`
    const response = await fetch(url);
    const body = await response.json();

    const data = body['data'];
    return !data.error && data.is_valid;
}

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