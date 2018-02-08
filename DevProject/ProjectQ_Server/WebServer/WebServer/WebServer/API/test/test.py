#from WebServer.API import database
import json
from WebServer import app
from flask_restful import Resource, Api
from flask_restful import reqparse, request

api = Api(app)


class Test(Resource):
    def get(self):
        return "hello server"


class TestPost(Resource):
    def post(self):
        parser = reqparse.RequestParser()
        parser.add_argument('test', type = str)
        parser.add_argument('test2', type = str)
        args = parser.parse_args()

        _data1 = args['test']
        _data2 = args['test2']
        print(_data1)
        print(_data2)
 
        return _data1, _data2


api.add_resource(Test, '/')
api.add_resource(TestPost, '/test')