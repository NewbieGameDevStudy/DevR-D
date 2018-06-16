from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
from Route import Common
import Entity.User
import time
from flask.helpers import make_response

class Login(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
            
        if not session in Entity.userCachedObjects:
            userObject = Entity.User.UserObject()
            Entity.userCachedObjects[session] = userObject
        
        try:
            return jsonify(self._getPlayerStatus(session))
        except Exception as e:
            print(str(e))
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
    
    def _getPlayerStatus(self, session):
        o_error = 0
        accountDB = DB.dbConnection.executeStoredProcedure("Game_Login", (session, o_error), (1, 1))
        #accountDB = DB.dbConnection.customSelectQuery("select * from gamedb.account where iAccountId = %s" % session)
        accountInfo = Entity.userCachedObjects[session].getData(Entity.Define.ACCOUNT_INFO)
        accountInfo.loadValueFromDB(accountDB[0])
        
        itemDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.item where iAccountId = %s" % session)
        inventoryDB = DB.dbConnection.customSelectQuery("select islot0, islot1 from gamedb.inventory where iAccountId = %s" % session)
        itemContanier = Entity.userCachedObjects[session].getData(Entity.Define.ITEM_CONTANIER)
        itemContanier.loadBasicInitDataFromDB(inventoryDB)
        itemContanier.loadValueFromDB(itemDB)
        
        mailDB = DB.dbConnection.customeSelectListQuery("select M.iIdx, M.iSenderAccountId, M.cSender, M.cTitle, M.cBody, M.dSendTime, M.dExpireTime, M.iReadDone, M.iMailType, A.iLevel, A.iportrait \
         from gamedb.mailBox AS M \
         LEFT JOIN gamedb.account AS A \
         ON M.iSenderAccountId = A.iAccountId \
         WHERE M.iAccountId = %s" % session)
        mailContanier = Entity.userCachedObjects[session].getData(Entity.Define.MAIL_CONTANIER)        
        mailContanier.loadValueFromDB(mailDB)
        
        #guildDB = DB.dbConnection.customSelectQuery("select * from gamedb.mail")
        guildMemberDB = DB.dbConnection.customeSelectListQuery("select * from gamedb.guild_member where iAccountId = %s" % session)
        
        if not guildMemberDB is None:
            d = 0
#         guildDB = DB.dbConnection.customSelectQuery("select islot0, islot1 from gamedb.inventory where iAccountId = %s" % session)
#         itemContanier = Entity.userCachedObjects[session].getData(Entity.Define.ITEM_CONTANIER)
#         itemContanier.loadBasicInitDataFromDB(inventoryDB)
#         itemContanier.loadValueFromDB(itemDB)
        
        Common.respHandler.mergeResp(accountInfo.getResp())
        Common.respHandler.mergeResp(itemContanier.getContainerResp())
        Common.respHandler.mergeResp(mailContanier.getContainerResp())
        
        return Common.respHandler.getResponse(Route.Define.OK_LOGIN_CONNECT)
                
                
    def put(self):
        Route.parser.add_argument("nickname")
        Route.parser.add_argument("portrait")
        args = Route.parser.parse_args()
        
        if not "nickname" in args or not args["nickname"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not "portrait" in args or not args["portrait"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        nickname = "\"%s\"" % args["nickname"] 
        portrait = args["portrait"]
        
        nickNameCheck = DB.dbConnection.customSelectQuery("select cname from gamedb.account where cname = %s" % nickname)
        
        if not nickNameCheck is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_ALREADY_CREATE_NICKNAME))
        
        accountId = Util.guidInst.createGuid()
        
        accountIdStr = str(accountId)

        userObject = Entity.User.UserObject()
        Entity.userCachedObjects[accountIdStr] = userObject
        #playerInfo = userObject.getData(Entity.Define.ACCOUNT_INFO)
        
        try:
            DB.dbConnection.customInsertQuery("insert into gamedb.account (iaccountid, cname, iportrait) values(%d, %s, %d)" % (int(accountId), nickname, int(portrait)))
        except Exception as e:
            print(str(e))
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_CREATE_NOT_LOGIN))
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_CREATE_LOGIN, {"accountId":accountId}))
    
    
    
class LogOut(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return
            
        if session in Entity.userCachedObjects:
            del Entity.userCachedObjects[session]
            print("logOut : %s" % session)