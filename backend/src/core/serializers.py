from django.db.models import Q

from rest_framework import serializers

from scores.models import Game
from scores.serializers import GameSerializer

from .models import School

import logging
logger = logging.getLogger('showdown.%s' % __name__)

class ShortSchoolSerializer(serializers.ModelSerializer):
    class Meta:
        model = School
        fields = ('slug', 'name', 'short_name', 'logo')

class SchoolSerializer(serializers.ModelSerializer):
    games = serializers.SerializerMethodField('get_latest_scores')

    def get_latest_scores(self, obj):
        games = Game.objects.filter(Q(home_team=obj) | Q(away_team=obj))
        return GameSerializer(games, many=True).data
    
    class Meta:
        model = School
        fields = ('slug', 'name', 'short_name', 'games', 'logo')