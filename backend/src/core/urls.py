from django.conf.urls import url

from rest_framework.urlpatterns import format_suffix_patterns

from .views import *

urlpatterns = [
    url(
        regex=r"^schools$",
        view=SchoolsView.as_view(),
        name="schools_view",
    ),
    url(
        regex=r"schools/(?P<school_slug>[A-Za-z0-9-]+)$",
        view=SchoolView.as_view(),
        name="school_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)