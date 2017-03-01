import * as Router from 'koa-router';
import * as winston from 'winston';

import { verifyFacebookAccessToken, generateJSONWebToken, UnauthenticatedUserError } from './auth';

interface LoginBody {
    accessToken: string;
}
async function loginRouteMiddleware(ctx: Router.IRouterContext, next: () => Promise<any> ) : Promise<any> {
    const { accessToken } = (ctx.request as any).body as LoginBody;
    const fbUserId = await verifyFacebookAccessToken(accessToken);
    
    if (!fbUserId) {
        winston.warn('Invalid Facebook access token used');
        ctx.status = 401;
        await next();
    }

    ctx.response.body = { token: await generateJSONWebToken(fbUserId, '')}; 
    winston.info(`User ${ fbUserId } logged in`);
    ctx.status = 200;
}

const router = new Router();

router.post('/login', loginRouteMiddleware);

export default router;