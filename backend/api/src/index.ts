import * as Koa from 'koa';

const app = new Koa();
app.use(async (ctx, next) => {
    ctx.body = 'Hello, Showdown 2017!';
});

app.listen(3000);