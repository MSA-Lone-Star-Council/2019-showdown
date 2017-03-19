from django.conf.urls import include, url
from django.contrib import admin

urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^accounts/', include('accounts.urls')),
    url(r'^events/', include('events.urls')),
    url(r'^scores/', include('scores.urls')),
    url(r'^core/', include('core.urls')),
    url(r'^docs/', include('rest_framework_docs.urls')),
]