from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
import Util
from Route.Common import RespBase

class PlayerInfo(Route.Common.RespBase):
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
        playerInfo = PlayerInfo()   
                
        if not 'accountId' in args or not args['accountId']:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_FAILED_PARAM))
            
        accountId = args['accountId']
        result = DB.dbConnection.selectQuery("gamedb.account", "uAccount", str(accountId), playerInfo.convertDBField())
        if not result:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
        
        resultJson = playerInfo.successToJson(result)
        session[str(resultJson['accountId'])] = resultJson['accountId']
        return jsonify(resultJson)
  
    def put(self):
        accountId = Util.guidInst.createGuid()
        session[str(accountId)] = accountId;
        
        # 실제 유저객체를 관리하는 것을 만들어야합니다.
        playerinfo = PlayerInfo()
        
        result = DB.dbConnection.insertQuery("gamedb.account", playerinfo.convertDBField())
        
        if not result:
            return jsonify(RespBase.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
                    
        return jsonify(RespBase.successResponse(Route.Define.SUCCESS_CREATE_LOGIN, {'accountId':accountId}))