from rest_framework import serializers

from events.serializers import BriefEventSerializer

from .models import Game, Score, ScoreCard

class ScoreSerializer(serializers.ModelSerializer):
    team = serializers.SlugRelatedField(slug_field='name', read_only=True)
    class Meta:
        model = Score
        fields = ('team', 'score')

class ScoreCardSerializer(serializers.ModelSerializer):
    game = serializers.PrimaryKeyRelatedField(read_only=True)
    scores = serializers.SerializerMethodField()

    def get_scores(self, obj):
        scores = Score.objects.filter(score_card=obj)
        return ScoreSerializer(scores, many=True).data
    
    class Meta:
        model = ScoreCard
        fields = ('id', 'game', 'scores')

class GameSerializer(serializers.ModelSerializer):
    event = serializers.PrimaryKeyRelatedField(read_only=True)
    teams = serializers.SlugRelatedField(slug_field='name', many=True, read_only=True)

    score = serializers.SerializerMethodField('get_latest_score')
    time = serializers.SerializerMethodField()

    def get_latest_score(self, obj):
        scorecard = ScoreCard.objects.filter(game=obj).latest()
        scores = Score.objects.filter(score_card=scorecard)
        return ScoreSerializer(scores, many=True).data
    
    def get_time(self, obj):
        scorecard = ScoreCard.objects.filter(game=obj).latest()
        return scorecard.time

    class Meta:
        model = Game
        fields = ('id', 'title', 'event', 'teams', 'score', 'time')

class GameEventSerializer(GameSerializer):
    event = BriefEventSerializer(read_only=True)