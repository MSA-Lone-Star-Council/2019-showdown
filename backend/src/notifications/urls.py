from django.conf.urls import url

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^annoucements$",
        view=AnnouncementView.as_view(),
        name="annoucement_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)