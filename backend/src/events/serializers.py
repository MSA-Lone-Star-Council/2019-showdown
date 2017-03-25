from rest_framework import serializers

from .models import Location, Event

class FullLocationSerializer(serializers.ModelSerializer):
    class Meta:
        model = Location
        fields = '__all__'

class BriefLocationSerializer(serializers.ModelSerializer):
    class Meta:
        model = Location
        fields = ('id', 'name')

class FullEventSerializer(serializers.ModelSerializer):
    location = FullLocationSerializer(read_only=True)

    class Meta:
        model = Event
        fields = '__all__'