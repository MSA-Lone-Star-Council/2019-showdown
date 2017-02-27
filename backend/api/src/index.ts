import * as Koa from 'koa';
import * as winston from 'winston';

import appConfig from './config';
import { authenticator, UserState, UnauthenticatedUserError } from './auth';

// Set up winston logging
winston.remove(winston.transports.Console);
winston.add(winston.transports.Console, {
    'timestamp': true,
    'colorize': true,
});


const app = new Koa();

// Basic logger that just logs incoming requests
app.use(async (ctx, next) => {
    winston.info(`${ctx.method} ${ctx.path}`);
    await next();
});

app.use(authenticator(appConfig.api.secret));

// Default route
app.use(async (ctx, next) => {
    const state = ctx.state as UserState
    if (!state.user){
        throw new UnauthenticatedUserError();
    }
    const name = state.user.sub;
    ctx.body = `Hello, ${ name }!!`;
});

winston.info('Starting server on port 3000');
app.listen(3000);