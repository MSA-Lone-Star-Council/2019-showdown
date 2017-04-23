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

In order to run the entire application in development:

0. Docker must be installed. 
1. Copy ```.env.example``` to ```.env``` and specify parameters
2. Run docker-compose,

```bash
docker-compose up # For development

# And then, in another shell
docker exec -it backend_api_1 ./manage.py migrate
docker exec -it backend_api_1 ./manage.py collectstatic
```
Visit ```localhost``` 

For production runs:
1. Set up a publicly exposed server (ports 80, 443, and 8080 should be accessible) with Docker
2. Configure your domain name registrar to point a domain or subdomain to the IP address of your server
3. Copy ```production.example.yml``` on your local machine to ```docker-compose.yml``` on the remote server and change ```example.com``` to your chosen domain/subdomain
4. Copy ```traefik.example.toml``` on your local machine to ```traefik.toml``` on the remote server and update ```example.com``` to your chosen domain/subdomain. Also change the email address (needs to be valid, but won't actually be used for anything - spam or otherwise)
5. Copy ```.env.example``` on your local machine to ```.env``` on the remote server and update values as needed. Be sure to change the secret key to anything else. The database and cache URLs should remain as-is if you are using the example production docker-compose.
6. And then start the application:
```bash
docker-compose -p backend up -d
docker exec -it backend_api_1 ./manage.py migrate
docker exec -it backend_api_1 ./manage.py collectstatic
# Wait roughly 30 seconds for application to start up before testing

# Viewing logs
docker-compose -p backend logs api -f
# Scaling horizontally
docker-compose -p backend scale api=3
```

## Building new image
``` bash 
docker build -t msalonestarcouncil/showdown:latest src
docker push msalonestarcouncil/showdown # If you want to publish it to the docker registry - will need permissions
```
