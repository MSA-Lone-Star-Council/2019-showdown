import * as Koa from 'koa';
import * as parse from 'co-body';
import * as winston from 'winston';

import appConfig from './config';
import { authenticator, UserState, UnauthenticatedUserError } from './auth';
import router from './routes';

// Set up winston logging
winston.remove(winston.transports.Console);
winston.add(winston.transports.Console, {
    'timestamp': true,
    'colorize': true,
});


const app = new Koa();
app.use(async (ctx, next) => {
    (ctx.request as any).body = await parse.json(ctx);
    next();
});

// Basic logger that just logs incoming requests
app.use(async (ctx, next) => {
    winston.info(`${ctx.method} ${ctx.path}`);
    await next();
});

app.use(authenticator(appConfig.api.secret));

// Default route
app.use(router.routes());
app.use(router.allowedMethods());

winston.info('Starting server on port 3000');
app.listen(3000);