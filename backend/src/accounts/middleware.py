import logging

from django.conf import settings

import jwt

logger = logging.getLogger('showdown.%s' % __name__)

class WebTokenMiddleware():
    def __init__(self, get_response):
        self.get_response = get_response
    
    def __call__(self, request):
        request.token = None
        if 'HTTP_AUTHORIZATION' in request.META:
            auth_header = request.META['HTTP_AUTHORIZATION']
            _, _, token = auth_header.partition(' ')
            try:
                request.token = jwt.decode(token, settings.SECRET_KEY)
            except jwt.DecodeError:
                logger.warn('Invalid JWT token')

        response = self.get_response(request)

        return response
