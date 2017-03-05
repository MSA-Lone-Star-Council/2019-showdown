import logging

import graphene
import jwt

from django.conf import settings

from util import facebook

logger = logging.getLogger('showdown.%s' % __name__)

class Viewer(graphene.ObjectType):
    class Input:
        jwtToken = graphene.String()

    name = graphene.String()

class CreateToken(graphene.Mutation):
    class Input:
        facebookAccessToken = graphene.String()
        
    token = graphene.String()
    error = graphene.String()

    @staticmethod
    def mutate(root, args, context, info):
        access_token = args.get('facebookAccessToken')

        try:
            user_id = facebook.get_token_info(access_token)
            claim = {
                'sub': user_id,
                'iss': 'http://texas-msa.org',
                'permission': '',
            }
            jwt_token = jwt.encode(claim, settings.SECRET_KEY).decode("utf-8")
            return CreateToken(token=jwt_token, error='')
        except Exception as e:
            logger.exception(e)
            return CreateToken(token='', error=str(e))


class Query(graphene.ObjectType):
    viewer = graphene.Field(Viewer, accessToken=graphene.String())    

    def resolve_viewer(self, args, context, info):
        token = args.get('accessToken')
        if token is None: return Viewer(name='')
        try:
            decoded_token = jwt.decode(token, settings.SECRET_KEY)
        except jwt.DecodeError:
            logger.warn('Invalid JWT token')
            return
        
        user_info = facebook.get_facebook_profile(decoded_token['sub'])
        return Viewer(name=user_info['name'])

class Mutation(graphene.ObjectType):
    create_token = CreateToken.Field()

schema = graphene.Schema(query=Query, mutation=Mutation)