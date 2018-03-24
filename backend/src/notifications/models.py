from django.db import models

class Announcement(models.Model):
    title = models.CharField(max_length=40)
    body = models.TextField()
    time = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return self.title