from Route.PlayerInfo import Login
from Route.PlayerInfo import RespPlayerInfo
from flask_restful import reqparse

route_dict = { Login:'/loginInfo' }

parser = reqparse.RequestParser()