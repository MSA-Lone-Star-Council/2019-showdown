# **LSC Showdown API server**
The API server that handles requests to the backend for the LSC
showdown mobile application

## Development Workflow
### First Time Setup

**These steps should be done inside a Python virtual environment!**

1. Requires Python 3.4+. Ensure that ```python``` and ```pip``` are in your path.
i.e. you can do the following in a terminal:
```bash
$ python --version
v3.5 # or 3.4.x+
$ pip --version
3.10.9 # or anything, shouldn't really matter
```
2. Install dependencies
```bash
# make sure you are inside the directory of this README ($REPO/backend/api)
$ pip install -r requiremnents.txt
``` 

### Adding new Python packages

**DO THIS IN A VIRTUALENV**

```pip install <packagename>```

Also add the package to the ```requirements.txt``` file, with the **exact** version

### Developing and Running

#### Running with Docker (Recommended)
Any code you write should be able to run inside a docker container, since this will be necessary
for deploying the application. Fortunately, this is pretty easy.

Following instructions are for Docker for Linux and Docker for Mac.
For Windows, you will need Windows 10 Pro with the Anniversary Update to run Docker for Windows. Make Powershell substitutions as appropriate.
(You could also just run a Linux VM....)

**If you change requirements.json, run the following to see your changes in the docker container!!!**
```bash
docker-compose up --build
```
To run with Docker and see your code changes (even without rebuilding the docker image!)
```bash
docker-compose up
```

### Running on your own machine
After making sure the ```.env``` file has the correct database connection string,
```bash
env $(cat .env | xargs) ./src/manage.py runserver
```

### Running Django shell
```bash
docker exec -it backend_api_1 ./manage.py shell
```

### Running Django migration
```bash
docker exec -it backend_api_1 ./manage.py migration
```