import logging

from django.conf import settings

from rest_framework.views import APIView
from rest_framework.response import Response

import jwt

from accounts.models import User

from util import facebook

logger = logging.getLogger('showdown.%s' % __name__)

class LoginView(APIView):
    def post(self, request, format=None):
        try:
            facebook_access_token = request.data['facebookAccessToken']
        except KeyError:
            return Response(status=400)

        try:
            user_id = facebook.get_token_info(facebook_access_token)
            
            user, created = User.objects.get_or_create(facebook_id=user_id)
            if created:
                profile = facebook.get_facebook_profile(user_id)
                user.name = profile['name']
                user.save()

            claim = {
                'sub': str(user.id),
                'iss': 'http://texas-msa.org',
                'permission': 'admin' if user.adminstrator else '',
                'meta': {
                    'name': user.name
                }
            }
            jwt_token = jwt.encode(claim, settings.SECRET_KEY).decode("utf-8")
            return Response({'token': jwt_token}, status=200)
        except Exception as e:
            logger.exception(e)
            return Response(status=401)