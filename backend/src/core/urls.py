from django.conf.urls import url
from django.views.decorators.cache import cache_page

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^schools/(?P<school_slug>[A-Za-z0-9-]+)/games$",
        view=cache_page(1)(SchoolView.as_view()),
        name="school_view",
    ),
    url(
        regex=r"^schools$",
        view=cache_page(120)(SchoolsView.as_view()),
        name="schools_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)