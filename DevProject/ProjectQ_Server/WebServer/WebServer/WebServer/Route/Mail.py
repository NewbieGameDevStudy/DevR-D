from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
from Route import Common
import Entity.User
from Entity import serverCachedObject, userCachedObjects

class MailPostRead(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        mailDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.mailBox where iAccountId = %s" % session)
        mailContanier = Entity.userCachedObjects[session].getData(Entity.Define.MAIL_CONTANIER)        
        mailContanier.loadValueFromDB(mailDB)
        
        Common.respHandler.mergeResp(mailContanier.getContainerResp())
        
        return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)
        
        
class MailWrite(Resource, Common.BaseRoute):
    def put(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("senderAccountId")
        Route.parser.add_argument("targetAccountId")
        Route.parser.add_argument("title")
        Route.parser.add_argument("body")
        args = Route.parser.parse_args()
        
        if not "senderAccountId" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        if not "targetAccountId" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        if not "title" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        if not "body" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        
#         mailDB = DB.dbConnection.customInsertQuery("insert into gamedb.mail iaccountid, isenderaccountid, csender, ctitle, cbody values(%d, %d, %s, %s, %s)" % session)
#         mailContanier = Entity.userCachedObjects[session].getData(Entity.Define.MAIL_CONTANIER)        
#         mailContanier.loadValueFromDB(mailDB)
#         
#         Common.respHandler.mergeResp(mailContanier.getContainerResp())
#         
#         return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)
        
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
        