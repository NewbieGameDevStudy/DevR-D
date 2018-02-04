"""
This script runs the WebServer application using a development server.
"""

from WebServer import app
from flask import Flask, jsonify
from flask_restful import fields, Resource, Api, marshal_with
from flask_restful import reqparse

#print(environ.get('SERVER_HOST', 'localhost'))
#print(environ.get('SERVER_PORT', '5555'))
#a = app.config['MYSQL_DATABASE_USER']

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
app.run("localhost", 5000)
