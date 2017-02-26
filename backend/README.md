# **Showdown 2017 Backend**
Backend for the Texas MSA Showdown 2017 mobile application.

## Features:
* Schedule
* Locations
* Sport scores
* Twitter stream

## Tech stack:
* Language: [TypeScript 2.2](https://www.typescriptlang.org/)
* Web framework: [Koa v2](http://koajs.com/)
* Database: [MongoDB](https://www.mongodb.com/)
* Deployment: [Docker](https://www.docker.com/), [docker-compose](https://docs.docker.com/compose/)
* Push notifications: [Azure Notification Hub](https://azure.microsoft.com/en-us/services/notification-hubs/)
* Hosting: [Azure VM](https://docs.microsoft.com/en-us/azure/virtual-machines/virtual-machines-linux-docker-compose-quickstart)


## Quickstart

In order to run the entire application:

0. Docker must be installed. 
1. Copy ```config.json.example``` to ```config.json``` and specify parameters
2. Run docker-compose,

```bash
docker-compose up # For development
docker-compose up -f production.yml # For production
```

## ```config.json``` Options

```javascript
{
    "api" : {
        "secret": "changeme"   // secret used to (en/de)crypt auth tokens. Can be any string
        "facebook": {
            "appID": "abcd1234"  // App ID to use for Facebook login validation, get it from https://developers.facebook.com
            "appSecret": "xyz987" // App secret to use for Facebook login validation, get it from https://developers.facebook.com
        }
    }
}
```
