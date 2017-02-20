
FROM node:latest
LABEL Name=showdown-17-api Version=1.0.0 
RUN npm install pm2 -g
COPY package.json /tmp/package.json
RUN cd /tmp && npm install --production
RUN mkdir -p /usr/src/app && mv /tmp/node_modules /usr/src
WORKDIR /usr/src/app
COPY ./dist/ /usr/src/app
EXPOSE 3000
EXPOSE 5858
CMD ["pm2-docker", "index.js"]
