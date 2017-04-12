from django.conf.urls import url
from django.views.decorators.cache import cache_page

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^schedule$",
        view=cache_page(1)(ScheduleView.as_view()),
        name="schedule_view",
    ),
    url(
        regex=r"^(?P<event_id>[0-9a-f-]+)/games$",
        view=cache_page(1)(GamesForEventView.as_view()),
        name="event_games_view",
    ),
    url(
        regex=r"^locations/(?P<location_id>\d+)$",
        view=cache_page(1)(LocationView.as_view()),
        name="location_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)