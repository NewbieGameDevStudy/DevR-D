from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
from Route import Common
from Route import Define
import Entity.User
from Entity import serverCachedObject, userCachedObjects

class SlotEquip(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
            
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
            
        Route.parser.add_argument("slotId")
        Route.parser.add_argument("itemIdx")
        args = Route.parser.parse_args()
        
        if not args["slotId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not args["itemIdx"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        itemIdx = int(args["itemIdx"])
        slotId = int(args["slotId"])
        
        userObject = userCachedObjects[session]
        itemContainer = userObject.getData(Entity.Define.ITEM_CONTANIER)
        returnCode = itemContainer.equipItem(slotId, itemIdx)
        
        o_error = 0
        if returnCode is Define.OK_EQUIP_ITEM:
            try:
                #DB.dbConnection.customInsertQuery("insert into gamedb.inventory (iAccountId, islot%d) values (%s, %d) on duplicate key update islot%d = %d" % (slotId, session, itemIdx, slotId, itemIdx))
                resultDB = DB.dbConnection.executeStoredProcedure("Game_Slot_Equip", (session, slotId, itemIdx, o_error), (3, 3))
            except Exception as e:
                print(str(e))
        else:
            return jsonify(Common.respHandler.errorResponse(returnCode))
        
        o_error = resultDB[0]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"equipItemIdx" : itemIdx, "slotId" : slotId}))
    
    
class SlotUnEquip(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
            
        Route.parser.add_argument("slotId")

        args = Route.parser.parse_args()
        
        if not args["slotId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        slotId = int(args["slotId"])
        
        userObject = userCachedObjects[session]
        itemContainer = userObject.getData(Entity.Define.ITEM_CONTANIER)
        
        slotItemId = itemContainer.getSlotItem(slotId)
        if slotItemId == 0:
            return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"unequipItemIdx" : slotItemId, "slotId" : slotId}))
        
        o_error = 0
        returnCode = itemContainer.unEquipItem(slotId, slotItemId)
        if returnCode is Define.OK_UNEQUIP_ITEM:
            try:
                #DB.dbConnection.customInsertQuery("insert into gamedb.inventory (iAccountId, islot%d) values (%s, 0) on duplicate key update islot%d = 0" % (slotId, session, slotId))
                resultDB = DB.dbConnection.executeStoredProcedure("Game_Slot_UnEquip", (session, slotId, o_error), (2, 2))
            except Exception as e:
                print(str(e))
        else:
            return jsonify(Common.respHandler.errorResponse(returnCode))
        
        o_error = resultDB[0]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        

        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"unequipItemIdx" : slotItemId, "slotId" : slotId}))
    
