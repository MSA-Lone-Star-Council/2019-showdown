from django.conf.urls import url

from rest_framework.urlpatterns import format_suffix_patterns

from .views import LoginView

urlpatterns = [
    url(
        regex=r"^login$",
        view=LoginView.as_view(),
        name="login_view",
    ),
]

urlpatterns = format_suffix_patterns(urlpatterns)