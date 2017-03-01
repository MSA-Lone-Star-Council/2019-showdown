import fetch from 'node-fetch';
import * as jwt from 'jsonwebtoken';
import * as winston from 'winston';

import config from './config';

const resolvers = {
    Query: {
        async viewer(root, { token }, context) {
            if(!token) return { name: 'test user'}

            const secret = config.api.secret;
            try
            {
                const decodedToken = jwt.verify(token, secret);
                const fbConfig = config.api.facebook;
                const appToken = `${ fbConfig.appID }|${ fbConfig.appSecret }`
                const url = `https://graph.facebook.com/v2.8/${ decodedToken.sub }?access_token=${ appToken }`
                const response = await fetch(url);
                const body = await response.json();

                return { name: body.name }
            } catch (err) {
                winston.warn('Invalid JWT token supplied');
            }

        }
    },
    Mutation: {
        async createToken(root, { facebookAccessToken }, context) {
            const fbConfig = config.api.facebook;
            const appToken = `${ fbConfig.appID }|${ fbConfig.appSecret }`
            const url = `https://graph.facebook.com/v2.8/debug_token?access_token=${ appToken }&input_token=${ facebookAccessToken }`
            const response = await fetch(url);
            const body = await response.json();

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