from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
import Util

class RespPlayerInfo(Route.Common.RespBase):
    def __init__(self):
        Route.Common.RespBase.__init__(self)
        self.level = 0
        self.exp = 0
        self.gameMoney = 0
        self.name = ''

class Login(Resource):
    def get(self):
        Route.parser.add_argument('accountId')
        args = Route.parser.parse_args()
        playerInfo = RespPlayerInfo()   
                
        if not 'accountId' in args or not args['accountId']:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_FAILED_PARAM))
            
        accountId = args['accountId']
        result = DB.dbConnection.selectQuery("gamedb.account", "uAccount", str(accountId), playerInfo.convertDBField())
        if not result:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
        
        return jsonify(playerInfo.successToJson(result))
  
    def post(self):
        Route.parser.add_argument('accountId', type=int)
        args = Route.parser.parse_args()
        
        for count in range(0, 1000):
            sessionKey = Util.guidInst.createGuid()
            dd = sessionKey
            session[str(sessionKey)] = 10
            
        return jsonify(1)

