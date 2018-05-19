from Route.Login import Login
from Route import Login
from flask_restful import reqparse

route_dict = { Login:'/loginInfo' }

parser = reqparse.RequestParser()