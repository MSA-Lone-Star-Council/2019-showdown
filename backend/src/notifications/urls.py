from django.conf.urls import url
from django.views.decorators.cache import cache_page

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^annoucements$",
        view=cache_page(1)(AnnouncementView.as_view()),
        name="annoucement_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)