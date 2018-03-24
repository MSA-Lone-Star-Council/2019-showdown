from django.shortcuts import get_object_or_404

from rest_framework import generics

import arrow

from scores.models import Game
from scores.serializers import GameSerializer

from .models import Event, Location
from .serializers import FullEventSerializer, FullLocationSerializer

class ScheduleView(generics.ListAPIView):
    serializer_class = FullEventSerializer

    def get_queryset(self):
        # Find all events that ended up to an hour ago or haven't ended yet
        cutoff = arrow.utcnow().replace(hours=-1).datetime
        return Event.objects.filter(end_time__gte=cutoff).order_by('start_time')

class LocationView(generics.RetrieveAPIView):
    queryset = Location.objects.all()
    lookup_url_kwarg = 'location_id'
    serializer_class = FullLocationSerializer

class GamesForEventView(generics.ListAPIView):
    serializer_class = GameSerializer

    def get_queryset(self):
        event_id = self.kwargs.get('event_id')
        event = get_object_or_404(Event, pk=event_id) 
        return event.game_set.all()