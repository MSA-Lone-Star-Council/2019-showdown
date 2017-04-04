from rest_framework import serializers

from core.models import School
from events.models import Location, Event
from scores.models import Game, Score
from accounts.models import User

class EventSerializer(serializers.ModelSerializer):
    class Meta:
        model = Event
        fields = '__all__'

class LocationSerializer(serializers.ModelSerializer):
    class Meta:
        model = Location
        fields = '__all__'

class GameSerializer(serializers.ModelSerializer):
    away_team = serializers.SlugRelatedField(slug_field='slug', queryset=School.objects.all())
    home_team = serializers.SlugRelatedField(slug_field='slug', queryset=School.objects.all())

    class Meta:
        model = Game
        fields = '__all__'

class ScoreSerializer(serializers.ModelSerializer):
    class Meta:
        model = Score
        fields = ('away_points', 'home_points', 'time', 'game')

class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ('id', 'name')