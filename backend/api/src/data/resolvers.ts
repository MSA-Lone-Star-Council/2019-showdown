import fetch from 'node-fetch';
import * as jwt from 'jsonwebtoken';
import * as winston from 'winston';

import config from '../config';
import { FacebookConnector, FacebookQuery } from '../connectors/facebook';

const resolvers = {
    Query: {
        async viewer(root, { token }, context) {
            const facebook: FacebookConnector = context.fbConnector;
            if(!token) return { name: ''}
            try
            {
                const decodedToken = jwt.verify(token, config.api.secret);
                const result = await facebook.get({ path: decodedToken.sub });
                return { name: result.name }
            } catch (err) {
                winston.warn('Invalid JWT token supllied');
            }

        }
    },

    Mutation: {
        async createToken(root, { facebookAccessToken }, context) {
            const facebook: FacebookConnector = context.fbConnector;

            const body = await facebook.get({
                path: 'debug_token',
                options: {
                    'input_token': facebookAccessToken
                }
            });

            const data = body['data'];
            if(!data) return { token: '', error: 'unknown error'};
            if(data.error) return { token: '', error: data.error };
            if(!data.is_valid) return { token: '', error: 'invalid access token'};

            const claim = { sub: data.user_id, iss: 'http://texas-msa.org', permissions: 'admin'};
            return {
                'token': jwt.sign(claim, config.api.secret),
                'error': ''
            }
        },
    },
};

export default resolvers;