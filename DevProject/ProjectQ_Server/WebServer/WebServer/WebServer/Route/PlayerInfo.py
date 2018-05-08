from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
import Util
import sys
from Route.Common import RespBase

class PlayerInfo(Route.Common.RespBase):
    def __init__(self):
        Route.Common.RespBase.__init__(self)
        self.accountId = 0
        self.level = 1
        self.exp = 0
        self.gameMoney = 0
        self.name = ''
        self.avatarType = 0
        self.bestRecord = 0
        self.winRecord = 0
        self.continueRecord = 0
        
        super(PlayerInfo, self).convertDBField()

class Login(Resource):
    def get(self):
        Route.parser.add_argument("accountId")
        args = Route.parser.parse_args()
        playerInfo = PlayerInfo()   
                
        if not "accountId" in args or not args["accountId"]:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_CREATE_LOGIN_PARAM))
            
        accountId = args["accountId"]
        result = DB.dbConnection.selectQuery("gamedb.account", "iAccountId", str(accountId), playerInfo.ig_queryStr)
        if not result:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
        
        return jsonify(playerInfo.successToJson(result))
  
    def put(self):
        Route.parser.add_argument("nickName")
        Route.parser.add_argument("avatarType")
        args = Route.parser.parse_args()
        
        if not "nickName" in args or not args["nickName"]:
            return jsonify(RespBase.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
        
        if not "avatarType" in args or not args["avatarType"]:
            return jsonify(RespBase.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
        
        nickName = args["nickName"]
        avatarType = args["avatarType"]
        
        accountId = Util.guidInst.createGuid()
        
        session[str(accountId)] = nickName;
        
        # 실제 유저객체를 관리하는 것을 만들어야합니다.
        playerinfo = PlayerInfo()
        
        result = DB.dbConnection.insertQuery("gamedb.account", playerinfo.ig_queryStr, playerinfo.convertDBFieldValue({"accountId":accountId, "name":nickName, "avatarType":avatarType}))
        
        if not result:
            return jsonify(RespBase.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
                    
        return jsonify(RespBase.successResponse(Route.Define.SUCCESS_CREATE_LOGIN, {'accountId':accountId}))