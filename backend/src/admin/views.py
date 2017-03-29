from django.shortcuts import get_object_or_404

from rest_framework import generics

from events.models import Event, Location

from scores.models import Game
from scores.serializers import GameSerializer

from .permissions import AdminPermission, ScorekeeperPermission
from .serializers import EventSerializer, LocationSerializer

class AllEventsView(generics.ListCreateAPIView):
    permission_classes = (AdminPermission,)
    queryset = Event.objects.order_by('-start_time')
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