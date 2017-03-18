from django.shortcuts import get_object_or_404

from rest_framework import generics

from .models import Game
from .serializers import GameEventSerializer, ScoreCardSerializer

class GamesView(generics.ListAPIView):
    serializer_class = GameEventSerializer
    queryset = Game.objects.all()

class ScoresForGameView(generics.ListAPIView):
    serializer_class = ScoreCardSerializer

    def get_queryset(self):
        game_id = self.kwargs.get('game_id')
        game = get_object_or_404(Game, pk=game_id)

        return game.scorecard_set.order_by('-time')