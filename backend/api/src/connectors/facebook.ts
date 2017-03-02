import fetch from 'node-fetch';
import * as DataLoader from 'dataloader';
import * as winston from 'winston';

import { FacebookConfig } from '../config'

const FACEBOOK_GRAPH_ROOT = 'https://graph.facebook.com/v2.8';

export interface FacebookQuery {
    path: string;
    options?: {[key: string]: string }
}

export class FacebookConnector {
    loader: DataLoader<FacebookQuery, any>;

    constructor(public config: FacebookConfig) {
        this.loader = new DataLoader<FacebookQuery,any>((queries) => {
            return Promise.all(queries.map(query => {
                let paramString=`access_token=${ config.appID }|${ config.appSecret }`
                for(const option in query.options){
                    const queryString = `${ option }=${ query.options[option] }`;
                        paramString = `${ paramString }&${ queryString }`
                }
                const url = `${ FACEBOOK_GRAPH_ROOT }/${ query.path }?${ paramString }`;
                return fetch(url).then(data => data.json());
            }));
        }, { batch: false })
    }

    get(query: FacebookQuery) {
        return this.loader.load(query);
    }
}