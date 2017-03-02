import fetch from 'node-fetch';
import * as jwt from 'jsonwebtoken';
import * as winston from 'winston';

import config from '../config';
import { Facebook } from '../model/facebook';

const resolvers = {
    Query: {
        async viewer(root, { token }, context) {
            const facebook: Facebook = context.facebook;
            if(!token) return { name: ''}

            let decodedToken;
            try
            {
                decodedToken = jwt.verify(token, config.api.secret);
            } catch (err) {
                winston.warn('Invalid JWT token supplied');
                return;
            }

            try {
                const result = await facebook.getUserInfo(decodedToken.sub);
                return { name: result.name }
            } catch (err) {
                winston.warn(err);
                return {
                    token: '',
                    error: err 
                }
            }

        }
    },

    Mutation: {
        async createToken(root, { facebookAccessToken }, context) {
            const facebook: Facebook = context.facebook;

            try {
                const user_id = await facebook.getTokenInfo(facebookAccessToken);
                const claim = { sub: user_id, iss: 'http://texas-msa.org', permissions: 'admin'};
                return {
                    'token': jwt.sign(claim, config.api.secret),
                    'error': ''
                }
            } catch (error) {
                winston.error(error);
                return {
                    'token': '',
                    'error': error
                }
            }
        },
    },
};

export default resolvers;