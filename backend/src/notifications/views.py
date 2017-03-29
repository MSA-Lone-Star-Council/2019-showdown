import json
import logging
from collections import namedtuple

from django.conf import settings

from rest_framework import generics

from .models import Announcement 
from .serializers import AnnouncementSerializer
from .notification_hub import NotificationHub

logger = logging.getLogger('showdown.%s' % __name__)

class AnnouncementView(generics.ListAPIView):
    queryset = Announcement.objects.order_by('-time')
    serializer_class = AnnouncementSerializer

NotificationOptions = namedtuple('NotificationOptions', ['title', 'subtitle', 'extra'])

def get_ios_payload(options):
    return {
        'aps': {
            'alert': {
                'title': options.title,
                'body': options.subtitle,
            }
        },
        'data': options.extra,
    }

def get_android_payload(options):
    options.extra['title'] = options.title
    options.extra['subtitle'] = options.subtitle
    return {
        'data': options.extra
    }

def send_notification(options, tags):
    hub = NotificationHub(connection_string=settings.AZURE_NH_CONNECTION_STRING, hub_name=settings.AZURE_NH_NAME, debug=1)    
    logger.info('Sending notification to APNS with options %s to tags "%s"', options, tags)
    hub.send_apple_notification(get_ios_payload(options), tags)
    logger.info('Sending notification to FCM with options %s to tags "%s"', options, tags)
    hub.send_gcm_notification(get_android_payload(options), tags)



