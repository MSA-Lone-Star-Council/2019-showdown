from rest_framework import generics

from .models import Announcement 
from .serializers import AnnouncementSerializer

class AnnouncementView(generics.ListAPIView):
    queryset = Announcement.objects.order_by('-time')
    serializer_class = AnnouncementSerializer