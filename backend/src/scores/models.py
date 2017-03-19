import uuid

from django.db import models

class Game(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    title = models.CharField(max_length=140)
    event = models.ForeignKey('events.Event', on_delete=models.CASCADE)
    teams = models.ManyToManyField('core.School')
    scorekeeper = models.ForeignKey('accounts.User', on_delete=models.SET_NULL, null=True)

class ScoreCard(models.Model):
    ''' Represents the score at a point of time '''
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    game = models.ForeignKey(Game, on_delete=models.CASCADE)
    teams = models.ManyToManyField('core.School', through='Score')
    time = models.DateTimeField()

    class Meta:
        get_latest_by = 'time'

class Score(models.Model):
    ''' Represents the score at a particular time for a particular team ''' 
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    score_card =  models.ForeignKey(ScoreCard, on_delete=models.CASCADE)
    team = models.ForeignKey('core.School', on_delete=models.CASCADE)
    score = models.FloatField()
