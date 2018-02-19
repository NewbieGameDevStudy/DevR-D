from WebServer import app
from WebServer.API import database
from flask_restful import Resource, Api, fields, marshal_with
from flask_restful import reqparse, request
from flask import Flask, jsonify
import MySQLdb
import json
from WebServer.config import MYSQL

print("test.py")

api = Api(app)


class Test(Resource):
    def get(self):
        odd = [1,2,3]
        data = {'a':123, 'list':odd, 'strTemp':"hello server"}
        return jsonify(data)


# sample function
def db_execution_request(**kwagrs):
    if kwagrs is None:
        return None

    db_query = database.DBConnection()
    sql = 'select user_seq from user_data where user_id = %d' % kwagrs['user_id']
    result = db_query.select_query(sql)
    print(result)
    
    if result is None:
        sql = 'insert into user_data (user_id, user_nick, create_date, recent_login, last_logout) values (%d, "%s", now(), now(), now())' % (kwagrs['user_id'], kwagrs['user_nick'])
        print(sql)
        db_query.execute_query(sql)
    

class TestPost(Resource):
    def post(self):
        parser = reqparse.RequestParser()
        parser.add_argument('userId', type = int)
        parser.add_argument('strNick', type = str)
        args = parser.parse_args()

        user_id = args['userId']
        user_nick = args['strNick']
        print(user_id)
        print(user_nick)
        
        db_execution_request(user_id = user_id, user_nick = user_nick)
        
        #return jsonify(data)
        return jsonify(args)


api.add_resource(Test, '/')
api.add_resource(TestPost, '/test')


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