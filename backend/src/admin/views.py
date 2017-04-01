import logging
from django.shortcuts import get_object_or_404

from rest_framework import generics

from notifications.views import NotificationOptions, send_notification
from notifications.models import Announcement
from notifications.serializers import AnnouncementSerializer
from events.models import Event, Location
from scores.models import Game
from scores.serializers import GameSerializer

from .permissions import AdminPermission, ScorekeeperPermission
from .serializers import EventSerializer, LocationSerializer

logger = logging.getLogger('showdown.%s' % __name__)

class AllEventsView(generics.ListCreateAPIView):
    permission_classes = (AdminPermission,)
    queryset = Event.objects.order_by('start_time')
    serializer_class = EventSerializer

class EventDetailView(generics.RetrieveUpdateDestroyAPIView):
    permission_classes = (AdminPermission,)
    queryset = Event.objects.all()
    lookup_url_kwarg = 'event_id'
    serializer_class = EventSerializer

class AllLocationsView(generics.ListCreateAPIView):
    permission_classes = (AdminPermission,)
    queryset = Location.objects.all()
    serializer_class = LocationSerializer

class LocationDetailView(generics.RetrieveUpdateDestroyAPIView):
    permission_classes = (AdminPermission,)
    queryset = Location.objects.all()
    lookup_url_kwarg = 'location_id'
    serializer_class = LocationSerializer

class AllGamesView(generics.ListCreateAPIView):
    permission_classes = (AdminPermission,)
    queryset = Game.objects.all()
    serializer_class = GameSerializer

class AllAnnouncementsView(generics.ListCreateAPIView):
    permission_classes = (AdminPermission,)
    queryset = Announcement.objects.order_by('-time')
    serializer_class = AnnouncementSerializer

    def create(self, request, *args, **kwargs):
        response = super(AllAnnouncementsView, self).create(request, *args, **kwargs)

        serializer = self.get_serializer(data=request.data)
        serializer.is_valid(raise_exception=False) # Should already be valid since super method checks
        announcement = serializer.data
        
        options = NotificationOptions(title=announcement['title'], subtitle='Announcement from LSC', extra = { 'type': 'announcement' })
        send_notification(options, '')

        return response