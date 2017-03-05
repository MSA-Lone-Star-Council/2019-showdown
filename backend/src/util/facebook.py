import requests

from django.conf import settings

FACEBOOK_GRAPH_ROOT = 'https://graph.facebook.com/v2.8'

def get_facebook_results(path, options=None):
    facebook_config = settings.FACEBOOK
    access_token = '%(app_id)s|%(app_secret)s' % facebook_config

    if not options: options = {}

    options['access_token'] = access_token
    url = '%s/%s' % (FACEBOOK_GRAPH_ROOT, path)
    response = requests.get(url, params=options)

    return response.json()

def get_facebook_profile(user_id):
    return get_facebook_results(path=user_id)

def get_token_info(accessToken):
    result = get_facebook_results(
        path='debug_token', 
        options={'input_token': accessToken}
    )
    data = result['data']
    if 'error' in data: raise Exception(data['error'])
    if not data['is_valid']: raise Exception('Invalid token')

    return data['user_id']