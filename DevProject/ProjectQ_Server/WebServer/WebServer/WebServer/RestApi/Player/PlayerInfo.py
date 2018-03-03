from flask_restful import reqparse, abort, Api, Resource, fields, marshal_with
import asyncio
from flask import Flask, jsonify
import MySQLdb
import json

from RestApi import api, app, dbConnection, asyncFunc
from ..AsyncIo import AsyncIO

parser = reqparse.RequestParser()

class RespPlayerInfo(object):
    def SetInfo(self, result, level, exp, name, gameMoney):
        self.result = result
        self.level = level
        self.exp = exp
        self.gameMoney = gameMoney
        self.name = name

    def serialize(self):
        return {
            'result':self.result,
            'level':self.level,
            'exp':self.exp,
            'gameMoney':self.gameMoney,
            'name':self.name,
        }

class Info(Resource):
    def get(self):
        parser.add_argument('accountId')
        args = parser.parse_args()
        id = args["accountId"]
        query = "select iLevel, iExp, cName, iGameMoney from gamedb.playerinfo where uAccountId = %s" %(id)
        
        result = asyncFunc.asyncSelectMethod(query)

        playerInfo = RespPlayerInfo()    
        if result == None:
            playerInfo.result = False
        else:
            playerInfo.SetInfo(True, result[0], result[1], result[2], result[3])

        return jsonify(playerInfo.serialize())

    def post(self):
        parser.add_argument('accountId', type=int)
        args = parser.parse_args()
        #id = args["accountId"]
        #query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        #result = dbConnection.select_query(query)
        return jsonify(1)

api.add_resource(Info, '/playerinfo')

