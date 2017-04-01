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
]

urlpatterns = format_suffix_patterns(urlpatterns)