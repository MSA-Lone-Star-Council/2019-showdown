import * as Koa from 'koa';
import * as Router from 'koa-router';
import * as bodyParser from 'koa-bodyparser';
import { graphqlKoa, graphiqlKoa } from 'graphql-server-koa';
import { makeExecutableSchema } from 'graphql-tools';
import * as winston from 'winston';

import { config } from './config';
import Schema from './schema';
import Resolvers from './resolvers';

// Set up winston logging
winston.remove(winston.transports.Console);
winston.add(winston.transports.Console, {
    'timestamp': true,
    'colorize': true,
});

// Load up configuration
const configPath: string = process.env.CONFIG_FILE || '/usr/config/config.json';
const appConfig = config(configPath);

const executableSchema = makeExecutableSchema({
    typeDefs: Schema,
    resolvers: Resolvers
});

const app = new Koa();
const router = new Router();

// Basic logger that just logs incoming requests
app.use(async (ctx, next) => {
    winston.info(`${ctx.method} ${ctx.path}`);
    await next();
});

app.use(bodyParser());

router.post('/graphql', graphqlKoa( { schema: executableSchema }));
router.get('/graphiql', graphiqlKoa({ endpointURL: '/graphql'}));
app.use(router.routes());
app.use(router.allowedMethods());

winston.info('Starting server on port 3000');
winston.info(process.version);
app.listen(3000);