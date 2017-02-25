import * as Koa from 'koa';
import * as winston from 'winston';

import { config } from './config';
import { authenticator, UserState, UnauthenticatedUserError } from './auth';

// Set up winston logging
winston.remove(winston.transports.Console);
winston.add(winston.transports.Console, {
    'timestamp': true,
    'colorize': true,
});

// Load up configuration
const configPath: string = process.env.CONFIG_FILE || '/usr/config/config.json';
const appConfig = config(configPath);

const app = new Koa();

// Basic logger that just logs incoming requests
app.use(async (ctx, next) => {
    winston.info(`${ctx.method} ${ctx.path}`);
    await next();
});

app.use(authenticator(appConfig.api.username, appConfig.api.hashedPassword));

// Default route
app.use(async (ctx, next) => {
    const state = ctx.state as UserState
    if (!state.user){
        throw new UnauthenticatedUserError();
    }
    const name = state.user.username;
    ctx.body = `Hello, ${ name }!!`;
});

winston.info('Starting server on port 3000');
app.listen(3000);