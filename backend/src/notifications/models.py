from django.db import models
from django.contrib.postgres.fields import JSONField

class Topic(models.Model):
    name = models.CharField(max_length=25, unique=True)

class Message(models.Model):
    header = models.CharField(max_length=80, blank=False)
    summary = models.CharField(max_length=80, blank=False)
    meta = JSONField()  # Dump mobile platform specific stuff in here

    topics = models.ManyToManyField(Topic)