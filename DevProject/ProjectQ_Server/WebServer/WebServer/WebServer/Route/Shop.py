from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
import Entity.User

from Entity.Define import ITEM_CONTANIER
from Route import Common
from Entity import serverCachedObject, userCachedObjects
from Entity import Define

class ShopBuyProduct(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))

        Route.parser.add_argument("buyProductId")
        Route.parser.add_argument("buyProductCount")
        args = Route.parser.parse_args()

        if not args["buyProductId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not args["buyProductCount"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))

        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INVALID_ACCESS))

        userObject = userCachedObjects[session]

        shop = serverCachedObject[Entity.Define.SHOP]
        
        buyId = int(args["buyProductId"])
        buyProductCount = int(args["buyProductCount"])
        
        respCode = shop.buyProduct(buyId, buyProductCount, userObject)
        
        if not respCode is Route.Define.OK_SHOP_BUY_PRODUCT:
            return jsonify(Common.respHandler.errorResponse(respCode))
        
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        itemContainer = userObject.getData(ITEM_CONTANIER)
        
        Common.respHandler.mergeResp(accountInfo.getResp())
        Common.respHandler.mergeResp(itemContainer.getContainerResp())
        
        return Common.respHandler.getResponse(Route.Define.OK_SHOP_BUY_PRODUCT)
        
        
        