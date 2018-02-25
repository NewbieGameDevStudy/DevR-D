from flask_restful import reqparse, abort, Api, Resource, fields, marshal_with
from flask import Flask, jsonify
import MySQLdb
import json
from RestApi import api, app

parser = reqparse.RequestParser()

class Info(Resource):
    def get(self):
        parser.add_argument('accountId', type=int)
        parser.add_argument('accountId2', type=int)
        args = parser.parse_args()
        #id = args["accountId"]
        #query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        #result = dbConnection.select_query(query)
        return jsonify(1)
    def post(self):
        parser.add_argument('accountId', type=int)
        args = parser.parse_args()
        #id = args["accountId"]
        #query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        #result = dbConnection.select_query(query)
        return jsonify(1)

api.add_resource(Info, '/playerinfo')

