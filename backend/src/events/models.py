from django.db import models

class Location(models.Model):
    name = models.CharField(max_length=100)
    address = models.CharField(max_length=500)
    latitude = models.FloatField()
    longitude = models.FloatField()
    notes = models.TextField()

class Event(models.Model):
    audience = models.CharField(max_length=8) # "brothers", "sisters", or "general" 
    start_time = models.DateTimeField()
    end_time = models.DateTimeField()
    description = models.TextField()


