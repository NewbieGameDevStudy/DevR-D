from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
from Route import Common
import Entity.User
from Entity import serverCachedObject, userCachedObjects

class TradeShop(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
    def put(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("buyId")
        args = Route.parser.parse_args()
        
        if not "buyId" in args:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        shop = serverCachedObject[Entity.Define.SHOP]
        resultCode = shop.buyProduct(args["buyId"])
        
        if not resultCode is Route.Define.OK_SUCCESS:
            return jsonify(Common.respHandler.errorResponse(resultCode))
        
        
        