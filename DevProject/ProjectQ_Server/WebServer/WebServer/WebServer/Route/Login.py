from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
from Route import Common
import Entity.User

class Login(Resource):
    def get(self):
        Route.parser.add_argument("accountId")
        args = Route.parser.parse_args()
                                    
        if not "accountId" in args or not args["accountId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
            
        accountId = args["accountId"]
        
        accountIdStr = str(accountId)
        if not accountIdStr in Entity.userCachedObjects:
            userObject = Entity.User.UserObject()
            Entity.userCachedObjects[accountIdStr] = userObject
            playerInfo = userObject.getData(Entity.Define.PLAYER_INFO)
        else:
            playerInfo = Entity.userCachedObjects[accountIdStr].getData(Entity.Define.PLAYER_INFO)
        
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
        
        nickname = "\"%s\"" % args["nickname"] 
        portrait = args["portrait"]
        
        nickNameCheck = DB.dbConnection.customSelectQuery("select cname from gamedb.account where cname = %s" % nickname)
        
        if not nickNameCheck is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_ALREADY_CREATE_NICKNAME))
        
        accountId = Util.guidInst.createGuid()
        
        accountIdStr = str(accountId)
        session[accountIdStr] = nickname;

        userObject = Entity.User.UserObject()
        Entity.userCachedObjects[accountIdStr] = userObject
        playerInfo = userObject.getData(Entity.Define.PLAYER_INFO)
        
        try:
            DB.dbConnection.insertQuery("gamedb.account", playerInfo.ig_queryStr, playerInfo.getRenewFieldDBCache({"accountId":accountId, "name":nickname, "portrait":portrait}))
        except:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_CREATE_LOGIN, {'accountId':accountId}))