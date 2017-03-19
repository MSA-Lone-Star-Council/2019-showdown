from django.shortcuts import get_object_or_404

from rest_framework import generics

from .models import School
from .serializers import SchoolSerializer

class SchoolsView(generics.ListAPIView):
    queryset = School.objects.order_by('name')
    serializer_class = SchoolSerializer

class SchoolView(generics.RetrieveAPIView):
    queryset = School.objects.all()
    lookup_url_kwarg = 'school_slug'
    lookup_field = 'slug'
    serializer_class = SchoolSerializer