from .common import *

DEBUG = env.bool('DJANGO_DEBUG', default=True)
TEMPLATES[0]['OPTIONS']['debug'] = DEBUG

SECRET_KEY = env('DJANGO_SECRET_KEY', default='lsc__w@vz-m#v3gm^l0xb97+pcwh4@&qi3)0l+3_y&(*csk0#ab%%24')