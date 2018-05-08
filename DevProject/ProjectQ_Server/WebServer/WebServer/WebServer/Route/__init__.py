from Route.PlayerInfo import Login
from Route.PlayerInfo import PlayerInfo
from flask_restful import reqparse

route_dict = { Login:'/loginInfo' }

parser = reqparse.RequestParser()