from datetime import datetime
from time import sleep

from core.models import School

ut = School(
    name='University of Texas at Austin', 
    slug='ut-austin', 
    logo='https://google.com',
    short_name='UT Austin')
ut.save()

utd = School(
    name='University of Texas at Dallas', 
    slug='ut-dallas', 
    logo='https://google.com',
    short_name='UT Dallas')
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

from scores.models import Game,  Score

finals = Game(
    title='Finals', 
    event=brothers_basketball,
    scorekeeper=zuhair,
    away_team=utd,
    home_team=ut,
    in_progress=True
)
finals.save()

initial_score = Score(game=finals, away_points=0, home_points=0)
initial_score.save()

print('Sleeping')
sleep(5) # Too lazy to figure out how to fast forward time

final_score = Score(game=finals, away_points=10, home_points=90)
final_score.save()

print('Complete')