import logging
import requests

from django.conf import settings

FACEBOOK_GRAPH_ROOT = 'https://graph.facebook.com/v2.8'

logger = logging.getLogger('showdown.%s' % __name__)

def get_facebook_results(path, options=None):
    facebook_config = settings.FACEBOOK
    #access_token = '%(app_id)s|%(app_secret)s' % facebook_config

    if not options: options = {}

    #options['access_token'] = access_token
    url = '%s/%s' % (FACEBOOK_GRAPH_ROOT, path)
    response = requests.get(url, params=options)

    return response.json()

def get_facebook_profile(user_id):
    return get_facebook_results(path=user_id)

def get_token_info(accessToken):
    result = get_facebook_results(
        path='me', 
        options={'access_token': accessToken}
    )
    return result['id'], result['name']
