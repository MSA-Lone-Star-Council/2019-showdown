import uuid

from django.db import models


class Score(models.Model):
    ''' Represents the score at a particular time''' 
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    game = models.ForeignKey('Game', on_delete=models.CASCADE, related_name='scores') 
    away_points = models.FloatField(default=0)
    home_points = models.FloatField(default=0)
    time = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ['-time']

class Game(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    title = models.CharField(max_length=140)
    event = models.ForeignKey('events.Event', on_delete=models.CASCADE)
    scorekeeper = models.ForeignKey('accounts.User', on_delete=models.SET_NULL, null=True)
    away_team = models.ForeignKey('core.School', on_delete=models.CASCADE, related_name='home_games')
    home_team = models.ForeignKey('core.School', on_delete=models.CASCADE, related_name='away_games')
    in_progress = models.BooleanField(default=True)

    def save(self, *args, **kwargs):
        if not self.scores.all():
            score = Score(game=self, away_points=0, home_points=0)
            score.save()
        super(Game, self).save(*args, **kwargs)