from django.db import models

class School(models.Model):
    name = models.CharField(max_length=80)
    slug = models.SlugField()
    logo = models.URLField()