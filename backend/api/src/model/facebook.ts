import { FacebookConnector } from '../connectors/facebook';

export class Facebook {
    constructor(public connector: FacebookConnector) {
    }
    async getUserInfo(userID: string) {
        return this.connector.get({
            path: userID
        });
    }
    async getTokenInfo(accessToken: string) {
        const result = await this.connector.get({
            path: '/debug_token',
            options: {
                'input_token': accessToken
            }
        });

        const data = result['data'];
        if(!data) throw new Error('Unknown error');
        if(data.error) throw new Error(data.error);
        if(!data.is_valid) throw new Error('Invalid token');

        return data.user_id;
    }
}