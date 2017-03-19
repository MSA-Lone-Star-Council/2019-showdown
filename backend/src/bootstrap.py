from datetime import datetime
from time import sleep

from core.models import School

ut = School(name='UT Austin', slug='ut-austin', logo='https://google.com')
ut.save()

utd = School(name='UT Dallas', slug='ut-dallas', logo='https://google.com')
utd.save()

from events.models import Event, Location

union_ballroom = Location(
    name='Union Ballroom', 
    address='Guadalupe street', 
    latitude=35.0,
    longitude=40.0,
    notes='Upstairs')
union_ballroom.save()


brothers_basketball = Event(
    title = 'Brothers\' Basketball',
    audience='brothers',
    start_time=datetime.now(),
    end_time=datetime.now(),
    description='B-BALL',
    location = union_ballroom,
)
brothers_basketball.save()

from accounts.models import User
zuhair = User(facebook_id='609420747', adminstrator=True)
zuhair.save()

from scores.models import Game, ScoreCard, Score

finals = Game(
    title='Finals', 
    event=brothers_basketball,
    scorekeeper=zuhair
)
finals.save()
finals.teams.add(ut)
finals.teams.add(utd)
finals.save()

initial_score = ScoreCard(game=finals, time=datetime.now())
initial_score.save()

ut_initial_score = Score(score_card=initial_score, team=ut, score=0)
ut_initial_score.save()

utd_initial_score = Score(score_card=initial_score, team=utd, score=0)
utd_initial_score.save()

print('Sleeping')
sleep(5) # Too lazy to figure out how to fast forward time

final_score = ScoreCard(game=finals, time=datetime.now())
final_score.save()

ut_final_score = Score(score_card=final_score, team=ut, score=90)
ut_final_score.save()

utd_final_score = Score(score_card=final_score, team=utd, score=10)
utd_final_score.save()

print('Complete')