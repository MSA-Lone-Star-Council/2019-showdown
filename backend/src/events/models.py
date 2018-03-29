from django.db import models

class Location(models.Model):
    name = models.CharField(max_length=100)
    address = models.CharField(max_length=500)
    latitude = models.FloatField()
    longitude = models.FloatField()
    notes = models.TextField()

    def __str__(self):
        return self.name

class Event(models.Model):
    title = models.CharField(max_length=80)
    audience = models.CharField(max_length=8) # "brothers", "sisters", or "general" 
    start_time = models.DateTimeField()
    end_time = models.DateTimeField()
    description = models.TextField(blank=True, null=True)
    location = models.ForeignKey(Location, blank=True, null=True)

    def __str__(self):
        return self.title


