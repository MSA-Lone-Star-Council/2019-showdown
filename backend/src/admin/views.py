import logging
from django.shortcuts import get_object_or_404

from rest_framework import generics, status
from rest_framework.response import Response

from notifications.views import NotificationOptions, send_notification
from notifications.models import Announcement
from notifications.serializers import AnnouncementSerializer
from events.models import Event, Location
from scores.models import Game
from accounts.models import User

from .permissions import AdminPermission, ScorekeeperPermission
from .serializers import EventSerializer, LocationSerializer, GameSerializer, UserSerializer, ScoreSerializer

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

class GameDetailView(generics.RetrieveUpdateDestroyAPIView):
    permission_classes = (AdminPermission,)
    queryset = Game.objects.all()
    lookup_url_kwarg = 'game_id'
    serializer_class = GameSerializer

class AllUsersView(generics.ListAPIView):
    permission_classes = (AdminPermission,)
    queryset = User.objects.all()
    serializer_class = UserSerializer

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

class ScorekeeperGamesView(generics.ListAPIView):
    permission_classes = (ScorekeeperPermission,)
    serializer_class = GameSerializer

    def get_queryset(self):
        token = self.request.token
        logger.info(token)
        games = Game.objects.filter(scorekeeper__id=token['sub'])
        return games

class ScorekeeperScoresView(generics.CreateAPIView):
    permission_classes = (ScorekeeperPermission,)
    serializer_class = ScoreSerializer

    def create(self, request, *args, **kwargs):
        data = request.data
        data['game'] = kwargs['game_id']
        serializer = self.get_serializer(data=data)
        serializer.is_valid(raise_exception=True)
        self.perform_create(serializer)
        headers = self.get_success_headers(serializer.data)

        return Response(serializer.data, status=status.HTTP_201_CREATED, headers=headers)