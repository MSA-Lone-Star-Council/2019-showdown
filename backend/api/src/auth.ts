import * as Koa from 'koa';
import * as bcrypt from 'bcrypt';
import * as winston from 'winston';

class User {
    username: string;
    constructor(username: string){
        this.username = username;
    }
}

export interface UserState {
    user?: User;
}

export class UnauthenticatedUserError implements Error {
    name: string = "UnauthenticatedUserError";
    message: string = "No authenticated user";
    stack: string;
}

/**
 * Extracts the username and password from a Basic Authentication header
 * For example: "Basic YWRtaW46cGEkJHcwcmQ=" returns { username: 'admin', password: 'pa$$w0rd'}
 * @param {string} authorizationHeader The value for the 'authorization' header
 * @returns {object} username and password encoded in 'authorization' header
 */
function extractUsernameAndPassword(authorizationHeader: string) : { username: string, password: string } {
    const authorizeHeader: string[] = authorizationHeader.split(' ');
    const [, base64string] = authorizeHeader;  // Ignores the string 'Basic'

    // base64 is decoded into a string of form username:password
    const [username, password] = Buffer.from(base64string, 'base64').toString('UTF-8').split(':');
    return {
        username,
        password
    }
}

/**
 * Create a middleware function for authenticating against a single username and password with Basic Auth
 * @param {string} username The username for logging in
 * @param {string} hashedPassword The encrypted password the user will use
 * @return {Koa.Middleware} Middleware function that reads Basic Auth headers and authenticates users
 */
export function authenticator(username: string, hashedPassword: string) : Koa.Middleware {
    return async function(ctx, next) {
        if (ctx.header['authorization']) {
            const { username, password: plaintextPassword } = extractUsernameAndPassword(ctx.header['authorization']);
            const valid = await bcrypt.compare(plaintextPassword, hashedPassword);
            if(valid) {
                winston.info(`User ${ username } authenticated successfully`);
                const userState: UserState = ctx.state as UserState;
                userState.user = new User(username); 
            }
            else {
                winston.warn(`User ${ username } failed to authenticate`);
            }
        }

        try {
            await next();
        }
        catch (e) {
            if (e instanceof UnauthenticatedUserError){
                ctx.response.status = 401;
                ctx.set('WWW-Authenticate', 'Basic realm="User Visible Realm"');
            }
        }
    }
}