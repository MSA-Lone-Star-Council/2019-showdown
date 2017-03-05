# **Showdown 2017 Backend**
Backend for the Texas MSA Showdown 2017 mobile application.

## Features:
* Schedule
* Locations
* Sport scores
* Push Notifications

## Tech stack:
* Language: Python 3.5
* Web framework: [Django](https://www.djangoproject.com)
* Database: [PostgreSQL](https://www.postgresql.org/)
* Deployment: [Docker](https://www.docker.com/), [docker-compose](https://docs.docker.com/compose/)
* Push notifications: [Azure Notification Hub](https://azure.microsoft.com/en-us/services/notification-hubs/)
* Hosting: [Azure VM](https://docs.microsoft.com/en-us/azure/virtual-machines/virtual-machines-linux-docker-compose-quickstart)


## Quickstart

In order to run the entire application:

0. Docker must be installed. 
1. Copy ```.env.example``` to ```.env``` and specify parameters
2. Run docker-compose,

```bash
docker-compose up # For development
docker-compose up -f production.yml # For production

# And then
docker exec -it backend_api_1 ./manage.py migrate
docker exec -it backend_api_1 ./manage.py collectstatic
```