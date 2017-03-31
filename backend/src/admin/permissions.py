from rest_framework import permissions

class AdminPermission(permissions.BasePermission):
    message = 'Must be an admin'

    def has_permission(self, request, view):
        return request.token and request.token['permission'] == 'admin'

class ScorekeeperPermission(permissions.BasePermission):
    message = 'Must be scorekeeper'

    def has_object_permission(self, request, view, game):
        return request.token and str(game.scorekeeper.pk) == request.token['sub']