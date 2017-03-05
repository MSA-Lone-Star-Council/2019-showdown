
FROM python:3.5.3
LABEL Name=showdown-17-api Version=1.0.0 
RUN pip install gunicorn
COPY requirements.txt /tmp/requirements.txt
RUN cd /tmp && pip install -r requirements.txt
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
COPY . /usr/src/app
EXPOSE 3000
CMD ["gunicorn", "config.wsgi", "--name", "api_server", "-b", ":3000", "-w", "3"]
