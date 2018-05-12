from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import sys
import Route.Common
from Route import Common
from Route.Common import ObjRespBase

class PlayerInfo(Common.ObjRespBase):
    def __init__(self):
        super(PlayerInfo, self).__init__()
        self.db_accountId = 0
        self.db_level = 1
        self.db_exp = 0
        self.db_gameMoney = 0
        self.db_name = ''
        self.db_portrait = 0
        self.db_bestRecord = 0
        self.db_winRecord = 0
        self.db_continueRecord = 0
        
        super(PlayerInfo, self).initFieldDBQueryCache()

class Login(Resource):
    def get(self):
        Route.parser.add_argument("accountId")
        args = Route.parser.parse_args()
        playerInfo = PlayerInfo()   
                
        if not "accountId" in args or not args["accountId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
            
        accountId = args["accountId"]
        try:
            result = DB.dbConnection.selectQuery("gamedb.account", "iAccountId", str(accountId), playerInfo.ig_queryStr)
        except:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
        
        return jsonify(Common.respHandler.getResponse("base", playerInfo.getConvertToResponse(result, Route.Define.OK_LOGIN_CONNECT)))
                
    def put(self):
        Route.parser.add_argument("nickname")
        Route.parser.add_argument("portrait")
        args = Route.parser.parse_args()
        
        if not "nickname" in args or not args["nickname"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
        
        if not "portrait" in args or not args["portrait"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
        
        nickName = args["nickname"]
        nickName = "\"%s\"" % nickName
        portrait = args["portrait"]
        
        nickNameCheck = DB.dbConnection.customSelectQuery("select cname from gamedb.account where cname = %s" % nickName)
        
        if not nickNameCheck is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_ALREADY_CREATE_NICKNAME))
        
        accountId = Util.guidInst.createGuid()
        
        session[str(accountId)] = nickName;
        
        # 실제 유저객체를 관리하는 것을 만들어야합니다.
        playerinfo = PlayerInfo()
        
        try:
            result = DB.dbConnection.insertQuery("gamedb.account", playerinfo.ig_queryStr, playerinfo.getRenewFieldDBCache({"accountId":accountId, "name":nickName, "portrait":portrait}))
        except:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
        
        playerinfo.getConvertToResponse(result)
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_CREATE_LOGIN, {'accountId':accountId}))