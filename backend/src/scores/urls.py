from django.conf.urls import url
from django.views.decorators.cache import cache_page

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^games/(?P<game_id>[0-9a-f-]+)/scores$",
        view=cache_page(1)(ScoresForGameView.as_view()),
        name="game_scores_view",
    ),
    url(
        regex=r"^games$",
        view=cache_page(1)(GamesView.as_view()),
        name="games_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)