from django.conf.urls import url

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^announcements$",
        view=AllAnnouncementsView.as_view(),
        name="all_announcements_view",
    ),
    url(
        regex=r"^events$",
        view=AllEventsView.as_view(),
        name="all_events_view",
    ),
    url(
        regex=r"^events/(?P<event_id>\d+)$",
        view=EventDetailView.as_view(),
        name="event_detail_view",
    ),
    url(
        regex=r"^locations$",
        view=AllLocationsView.as_view(),
        name="all_location_view",
    ),
    url(
        regex=r"^locations/(?P<location_id>\d+)$",
        view=LocationDetailView.as_view(),
        name="location_detail_view",
    ),
    url(
        regex=r"^games$",
        view=AllGamesView.as_view(),
        name="all_games_view",
    ),
    url(
        regex=r"^games/(?P<game_id>[0-9a-f-]+)$",
        view=GameDetailView.as_view(),
        name="game_detail_view",
    ),
    url(
        regex=r"^users$",
        view=AllUsersView.as_view(),
        name="all_users_view",
    ),
    url(
        regex=r"^scorekeeper/games$",
        view=ScorekeeperGamesView.as_view(),
        name="scorekeeper_games",
    ),
    url(
        regex=r"^scorekeeper/games/(?P<game_id>[0-9a-f-]+)$",
        view=ScorekeeperScoresView.as_view(),
        name="scorekeeper_scores",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)