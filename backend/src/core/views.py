import logging

from django.shortcuts import get_object_or_404
from django.db.models import Q

from rest_framework import generics

from scores.models import Game
from scores.serializers import GameSerializer

from .models import School
from .serializers import SchoolSerializer, ShortSchoolSerializer

logger = logging.getLogger('showdown.%s' % __name__)

class SchoolsView(generics.ListAPIView):
    queryset = School.objects.order_by('name')
    serializer_class = ShortSchoolSerializer

class SchoolView(generics.ListAPIView):
    serializer_class = GameSerializer

    def get_queryset(self):
        school_slug = self.kwargs.get('school_slug')
        school = get_object_or_404(School, slug=school_slug)
        games = Game.objects.filter(Q(home_team=school) | Q(away_team=school))
        return games