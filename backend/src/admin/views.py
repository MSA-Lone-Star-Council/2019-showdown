import logging
from django.shortcuts import get_object_or_404

from rest_framework import generics, status, views
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
        games = Game.objects.filter(scorekeeper__id=token['sub'])
        return games

class ScorekeeperScoresView(generics.CreateAPIView):
    permission_classes = (ScorekeeperPermission,)
    serializer_class = ScoreSerializer

    def create(self, request, *args, **kwargs):
        game = get_object_or_404(Game, pk=kwargs['game_id'])

        data = request.data
        data['game'] = kwargs['game_id']
        serializer = self.get_serializer(data=data)
        serializer.is_valid(raise_exception=True)
        self.perform_create(serializer)
        headers = self.get_success_headers(serializer.data)
        
        score = serializer.data
        options = NotificationOptions(
            title='%s %d v %s %d' % (
                game.away_team.short_name, 
                score['away_points'],
                game.home_team.short_name,
                score['home_points']
            ),
            subtitle='%s - %s' % (
                game.event.title,
                "Live" if game.in_progress else "Final",
            ),
            extra = {
                'type': 'score',
                'game_id': str(game.id),
            }
        )
        logger.info(options)
        
        tags = "game_%s || school_%s || school_%s" % (
            game.id,
            game.away_team.slug,
            game.home_team.slug)
        send_notification(options, tags)

        return Response(score, status=status.HTTP_201_CREATED, headers=headers)

class GameInProgressView(views.APIView):
    def put(self, request, game_id, format=None):
        if not request.token:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

        game = get_object_or_404(Game, pk=game_id)
        game.in_progress = request.data['in_progress']
        game.save()
        resp = Response({'in_progress': game.in_progress}, headers={})
        return resp