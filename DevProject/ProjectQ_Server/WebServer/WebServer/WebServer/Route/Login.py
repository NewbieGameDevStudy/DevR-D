from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
from Route import Common
import Entity.User
import time

class Login(Resource):
    def get(self):
        Route.parser.add_argument("accountId")
        args = Route.parser.parse_args()
                                    
        if not "accountId" in args or not args["accountId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_LOGIN_PARAM))
            
        accountId = args["accountId"]
                
        accountIdStr = str(accountId)
        
        if not accountId in Entity.userCachedObjects:
            userObject = Entity.User.UserObject()
            Entity.userCachedObjects[accountId] = userObject
        
        try:
            return jsonify(self._getPlayerStatus(accountIdStr))
            
        except:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
    
    def _getPlayerStatus(self, accountId):
        accountDB = DB.dbConnection.customSelectQuery("select * from gamedb.account where iAccountId = %s" % accountId)
        accountInfo = Entity.userCachedObjects[accountId].getData(Entity.Define.ACCOUNT_INFO)
        accountInfo.updateValue(accountDB)
        
        itemDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.item where iAccountId = %s" % accountId)
        itemContanier = Entity.userCachedObjects[accountId].getData(Entity.Define.ITEM_CONTANIER)
        itemContanier.updateContainer(itemDB)
        
        mailDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.mailBox where iAccountId = %s" % accountId)        
        
        Common.respHandler.mergeResp(accountInfo.getResp())
        Common.respHandler.mergeResp(itemContanier.getContainerResp())
        
        return Common.respHandler.getResponse(Route.Define.OK_LOGIN_CONNECT)
                
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
        playerInfo = userObject.getData(Entity.Define.ACCOUNT_INFO)
        
        try:
            DB.dbConnection.insertQuery("gamedb.account", playerInfo.ig_queryStr, playerInfo.getRenewFieldDBCache({"accountId":accountId, "name":nickname, "portrait":portrait}))
        except:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_CREATE_LOGIN, {'accountId':accountId}))