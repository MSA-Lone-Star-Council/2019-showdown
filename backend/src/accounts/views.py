import logging

from django.conf import settings

from rest_framework.views import APIView
from rest_framework.response import Response

import jwt

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
            claim = {
                'sub': user_id,
                'iss': 'http://texas-msa.org',
                'permission': '',
            }
            jwt_token = jwt.encode(claim, settings.SECRET_KEY).decode("utf-8")
            return Response({'token': jwt_token}, status=200)
        except Exception as e:
            logger.exception(e)
            return Response(status=401)