import * as Koa from 'koa';
import * as winston from 'winston';

import { config } from './config';

const app = new Koa();

app.use(async (ctx, next) => {
    const start : any = new Date();
    next();
    const ms = (new Date() as any) - start;
    winston.info(`Request for ${ ctx.path } completed in ${ ms } ms`);
});

app.use(async (ctx, next) => {
    ctx.body = 'Hello, Showdown 2017!';
});

const configPath: string = process.env.CONFIG_FILE || '/usr/config/config.json';
config(configPath); // Initializes config

winston.info('Starting server on port 3000');
app.listen(3000);