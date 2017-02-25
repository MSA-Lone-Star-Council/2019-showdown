# **LSC Showdown API server**
The API server that handles requests to the backend for the LSC
showdown mobile application

## Development Workflow
### First Time Setup
1. Requires Node 6+. Ensure that ```node``` and ```npm``` are in your path.
i.e. you can do the following in a terminal:
```bash
$ node --version
v7.1.0 # or 6.x.x+
$ npm --version
3.10.9 # or anything, shouldn't really matter
```
2. Install dependencies
```bash
# make sure you are inside the directory of this README ($REPO/backend/api)
$ npm install 
``` 

### Adding new NPM packages

Ask yourself, is this package needed in production?

* Yes: ```npm install $package_name --save```
* No: ```npm install $package_name --save-dev```

### Developing and Running
All the code is written in TypeScript 2.2. Since you can't run .ts files directly, you'll need to compile them to
JavaScript first. You can use the ```tsc``` compiler directly, or compile from Visual Studio Code (I recommend the latter)

#### Running with Docker (Recommended)
Any code you write should be able to run inside a docker container, since this will be necessary
for deploying the application. Fortunately, this is pretty easy.

Following instructions are for Docker for Linux and Docker for Mac.
For Windows, you will need Windows 10 Pro with the Anniversary Update to run Docker for Windows. Make Powershell substitutions as appropriate.
(You could also just run a Linux VM....)

**If you change package.json, run the following to see your changes in the docker container!!!**
```bash
docker build -t showdown-backend:dev -f Dockerfile .
```
To run with Docker and see your code changes (even without rebuilding the docker image!)
```bash
docker run --rm -it -p 3000:3000 -v `pwd`/dist:/usr/src/app showdown-backend:dev
```
When you recompile the TypeScript code, you should see nodemon reload the server

### Running on your own machine
```bash
# Make sure config.json actually exists and is properly filled out
CONFIG_FILE=config.json ./node_modules/.bin/nodemon ./dist/index.js
```
Since every computer is a special snowflake, no guarantees that this will run correctly