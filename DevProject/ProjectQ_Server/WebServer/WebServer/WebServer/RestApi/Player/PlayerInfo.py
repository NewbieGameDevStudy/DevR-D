from flask_restful import reqparse, abort, Api, Resource, fields, marshal_with
import asyncio
from flask import Flask, jsonify
import MySQLdb
import json

from RestApi import api, app, dbConnection

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

async def GetInfo(accountId, future):
    query = "select iLevel, iExp, cName, iGameMoney from gamedb.playerinfo where uAccountId = %s" %(accountId)
    result = dbConnection.select_query(query)
    future.set_result(result)

class Info(Resource):
    def get(self):
        parser.add_argument('accountId')
        args = parser.parse_args()
        id = args["accountId"]
        #query = "select iLevel, iExp, cName, iGameMoney from gamedb.playerinfo where uAccountId = %s" %(id)
        #result = DataBase.dbConnection.select_query(query)
        loop = asyncio.get_event_loop()
        future = asyncio.Future()
        asyncio.ensure_future(GetInfo(id, future))
        loop.run_until_complete(future)
        result = future.result()
        loop.close()

        playerInfo = RespPlayerInfo()    
        if result == None:
            playerInfo.SetInfo(False, -1, -1, 'none', -1)
        else:
            playerInfo.SetInfo(True, result[0], result[1], result[2], result[3])

        rr = json.dumps(playerInfo.__dict__)
        #return json.dumps(playerInfo.__dict__)
        return jsonify(playerInfo.serialize())

    def post(self):
        parser.add_argument('accountId', type=int)
        args = parser.parse_args()
        #id = args["accountId"]
        #query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        #result = dbConnection.select_query(query)
        return jsonify(1)

api.add_resource(Info, '/playerinfo')

