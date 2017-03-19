from rest_framework import serializers

from scores.models import Game
from scores.serializers import GameEventSerializer

from .models import School


class SchoolSerializer(serializers.ModelSerializer):
    games = serializers.SerializerMethodField('get_latest_scores')

    def get_latest_scores(self, obj):
        games = Game.objects.filter(teams=obj)
        return GameEventSerializer(games, many=True).data
    
    class Meta:
        model = School
        fields = ('slug', 'name', 'games', 'logo')