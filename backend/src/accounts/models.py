import logging
import uuid

from django.db import models

from util import facebook

logger = logging.getLogger('showdown.%s' % __name__)

class User(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    facebook_id = models.CharField(max_length=40)
    name = models.CharField(max_length=200)
    adminstrator = models.BooleanField(default=False)

    def save(self, *args, **kwargs):
        if not self.name:
            profile = facebook.get_facebook_profile(self.facebook_id)
            self.name = profile['name']
            logger.info('Fetching name for user %s' % self.id)

        super(User, self).save(*args, **kwargs)
