import uuid

from django.db import models

# Create your models here.

class User(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    facebook_id = models.CharField(max_length=40)
    name = models.CharField(max_length=200)
    adminstrator = models.BooleanField(default=False)
