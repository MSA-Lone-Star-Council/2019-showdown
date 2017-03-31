from rest_framework import serializers

from events.serializers import FullEventSerializer
from core.models import School

from .models import Game, Score

# Redefining to avoid circular dependency between apps
class ShortSchoolSerializer(serializers.ModelSerializer):
    class Meta:
        model = School
        fields = ('slug', 'name', 'short_name', 'logo')

class ScoreSerializer(serializers.ModelSerializer):
    class Meta:
        model = Score
        fields = ('away_points', 'home_points', 'time')
    
class GameSerializer(serializers.ModelSerializer):
    event = FullEventSerializer(read_only=True)
    away_team = ShortSchoolSerializer(read_only=True)
    home_team = ShortSchoolSerializer(read_only=True)

    score = serializers.SerializerMethodField()

    def get_score(self, obj):
        return ScoreSerializer(obj.scores.first()).data

    class Meta:
        model = Game
        fields = ('id', 'title', 'event', 'away_team', 'home_team', 'score', 'in_progress')