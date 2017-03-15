from django.db import models

class Game(models.Model):
    title = models.CharField(max_length=140)
    event = models.ForeignKey('events.Event', on_delete=models.CASCADE)
    home = models.ForeignKey('core.School', on_delete=models.SET_NULL, related_name='home_games', null=True)
    away = models.ForeignKey('core.School', on_delete=models.SET_NULL, related_name='away_games', null=True)
    scorekeeper = models.ForeignKey('accounts.User', on_delete=models.SET_NULL, null=True)

class Score(models.Model):
    game = models.ForeignKey('Game', on_delete=models.CASCADE)
    home_score = models.IntegerField()
    away_score = models.IntegerField()
    time = models.DateTimeField(auto_now_add=True)
