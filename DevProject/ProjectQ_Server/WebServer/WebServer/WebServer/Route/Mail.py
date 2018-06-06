from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
import Entity.User
from Route import Common
from Entity import Define
from Entity import serverCachedObject, userCachedObjects

class MailPostRead(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        try:
            mailDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.mailBox where iAccountId = %s" % session)
        except Exception as e:
            print(str(e))
            return Route.Define.ERROR_DB
        
        mailContanier = Entity.userCachedObjects[session].getData(Entity.Define.MAIL_CONTANIER)        
        mailContanier.loadValueFromDB(mailDB)
        
        Common.respHandler.mergeResp(mailContanier.getContainerResp())
        return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)        
        
class MailWrite(Resource, Common.BaseRoute):
    def put(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("targetNickName")
        Route.parser.add_argument("title")
        Route.parser.add_argument("body")
        args = Route.parser.parse_args()
        
        if not args["targetNickName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        if not args["title"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        if not args["body"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        senderAccountId = int(session)
        targetNickName = args["targetNickName"]
        title = args["title"]
        body = args["body"]        
        
        userObject = userCachedObjects[session]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if accountInfo.dailyMailCount > Define.MAX_DAILY_MAIL_COUNT:
            if accountInfo.gameMoney <= 100:
                return Common.respHandler.getResponse(Route.Define.ERROR_NOT_ENOUGH_MONEY)
            accountInfo.gameMoney -= 100
        else:
            accountInfo.dailyMailCount += 1
        
        o_error = 0
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Mail_Write", (targetNickName, senderAccountId, title, body, accountInfo.gameMoney, o_error), (5, 5))
        except Exception as e:
            print(str(e))
            return Route.Define.ERROR_DB
        
        o_error = resultDB[1]
        
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_NOT_WRITE)
                
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"mailCount" : accountInfo.dailyMailCount, "gameMoney" : accountInfo.gameMoney})
        
class MailPostAccept(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("buyId")
        args = Route.parser.parse_args()
        
        if not "buyId" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INVALID_ACCESS))
        
        userObject = userCachedObjects[session]
        
        shop = serverCachedObject[Entity.Define.SHOP]
        resp = shop.buyProduct(int(args["buyId"]), userObject)
        return jsonify(resp)
        