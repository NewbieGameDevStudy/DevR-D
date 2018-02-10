#from WebServer.API import database
import json
from WebServer import app
from flask_restful import Resource, Api, fields, marshal_with
from flask_restful import reqparse, request
from flask import Flask, jsonify

print("test.py")

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
 
        return "Success!!"


api.add_resource(Test, '/')
api.add_resource(TestPost, '/test')

print()

#app = Flask(__name__)
#api = Api(app)

#test = {
#    'name': fields.String
#}

#odd = [1,2,3]

#test_feilds = {
#    'int' : fields.Integer,
#}

#class Call(object):
#    def __init__(self, test):
#        self.test = test

#class CreateUser(Resource) :
#    #@marshal_with(test_feilds)
#    def post(self, **kwargs):
#        parser = reqparse.RequestParser()
#        parser.add_argument("test");
#        args = parser.parse_args()
#        dd = args['test']
#        data = {'a': 321}
#        return jsonify(data)
#        #return Call(test = 100)
#    def get(self):
#        data = {'a': 321, 'list':odd}
#        return jsonify(data)

#api.add_resource(CreateUser, '/test')