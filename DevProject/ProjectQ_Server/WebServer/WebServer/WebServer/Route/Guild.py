# coding=utf8

from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Util
import Route.Common
import Entity.User
from Route import Common
from Entity import Define
from Entity import userCachedObjects
from Entity.Define import MAX_CREATE_GUILD_MONEY

class GuildList(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        userObject = userCachedObjects[session]
        guildContainer = userObject.getData(Define.GUILD_CONTANIER)
        
        guildIdx = guildContainer.idx
        try:
            if guildIdx == 0:
                guildDB = DB.dbConnection.customSelectQuery("select iguildIdx from gamedb.guild_member where iAccountId = %s" % session)
                guildIdx = guildDB[0]
                    
            if guildIdx == 0:
                return Common.respHandler.getResponse(Route.Define.OK_NOT_FOUND_JOIN_GUILD)
            
            guildMemberDB = DB.dbConnection.customeSelectListQuery("SELECT A.iAccountId, A.cName, A.iLevel, A.iExp, A.iportrait, A.ibestRecord, A.iwinRecord, A.icontinueRecord \
            FROM gamedb.guild_member AS M \
            LEFT JOIN gamedb.account AS A \
            ON M.iAccountId = A.iAccountId WHERE iguildIdx = %s" % guildIdx)
            
            guildInfoDB = DB.dbConnection.customSelectQuery("select * from gamedb.guild where iguildIdx = %s" % guildIdx)
                
        except Exception as e:
            print(str(e))
            return Common.respHandler.errorResponse(Route.Define.ERROR_DB)
        
        guildContainer = userObject.getData(Define.GUILD_CONTANIER)
        guildContainer.loadBasicInitDataFromDB(guildInfoDB)
        guildContainer.loadValueFromDB(guildMemberDB)
        
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, guildContainer.getContainerResp())  


class GuildCreate(Resource, Common.BaseRoute):
    def put(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("guildName")
        Route.parser.add_argument("guildJoinType")
        Route.parser.add_argument("guildMark")
        args = Route.parser.parse_args()
        
        if not args["guildName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not args["guildJoinType"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not args["guildMark"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        userObject = userCachedObjects[session]
        guildContainer = userObject.getData(Define.GUILD_CONTANIER)
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if guildContainer.idx > 0:
            return Common.respHandler.errorResponse(Route.Define.ERROR_CURRENT_JOIN_GUILD)
        
        if accountInfo.level <= 15:
            return Common.respHandler.errorResponse(Route.Define.ERROR_LOW_LEVEL)
        
        if accountInfo.gameMoney < 10000:
            return Common.respHandler.errorResponse(Route.Define.ERROR_NOT_ENOUGH_MONEY)
        
        createGuildName = args["guildName"]
        guildJoinType = int(args["guildJoinType"])
        guildMark = int(args["guildMark"])
        
        try:
            guildNameCheck = DB.dbConnection.customSelectQuery("select cguildName from gamedb.guild where cguildName = \"%s\"" % createGuildName)
        except Exception as e:
            print(str(e))
            return Common.respHandler.errorResponse(Route.Define.ERROR_DB)
        
        if not guildNameCheck is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_ALREADY_GUILDNAME))
        
        o_error = 0
        o_guildIdx = 0 
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Guild_Create", (accountInfo.accountId, createGuildName, guildJoinType, guildMark, MAX_CREATE_GUILD_MONEY, o_error, o_guildIdx), (5, 6))
        except Exception as e:
            print(str(e))
            return Common.respHandler.errorResponse(Route.Define.ERROR_DB)
        
        o_error = resultDB[0]
        o_guildIdx = resultDB[1]
        
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_NOT_CREATE_GUILD)
        
        guildContainer.updateGuild(o_guildIdx, createGuildName, guildJoinType, guildMark, accountInfo.accountId)
        
        memberInfo = Entity.Guild.GuildMemberInfo()
        memberInfo.accountId = accountInfo.accountId
        memberInfo.name = accountInfo.name
        memberInfo.level = accountInfo.level
        memberInfo.exp = accountInfo.exp
        memberInfo.portrait = accountInfo.portrait
        memberInfo.bestRecord = accountInfo.bestRecord
        memberInfo.winRecord = accountInfo.winRecord
        memberInfo.continueRecord = accountInfo.continueRecord
        memberInfo.syncToResp()
        guildContainer.setGuildMemberInfo(memberInfo)
        
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, guildContainer.getResp())        
        
        
class GuildJoin(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("guildName")
        Route.parser.add_argument("guildIdx")
        args = Route.parser.parse_args()
        
        if not args["guildName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        if not args["guildIdx"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))

        #checkGuildJoin
        joinGuildName = args["guildName"]
        joinGuildIdx = int(args["guildIdx"])
        guildCheckDB = DB.dbConnection.customSelectQuery("select iguildMemberCount, iguildJoinType from gamedb.guild where cguildName = \"%s\"" % joinGuildName)        
                
        if guildCheckDB is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_JOIN_GUILD))
        
        joinGuildMemberCount = guildCheckDB[0]
        if joinGuildMemberCount >= 20:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_FULL_JOIN_GUILDMEMBER))
        
        joinGuildType = guildCheckDB[1]
        
        userObject = userCachedObjects[session]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if joinGuildType == 1:
            title = "가입요청"
            senderMsg = "[%s]님이 길드에 가입요청을 하였습니다." % accountInfo.name
        elif joinGuildType == 0:
            title = "가입정보"
            senderMsg = "[%s]님이 길드에 가입을 하였습니다." % accountInfo.name 
        
        o_error = 0
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Guild_Join", (accountInfo.accountId, accountInfo.name, joinGuildName, title, senderMsg, o_error), (5, 5))
        except Exception as e:
            print(str(e))
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        o_error = resultDB[0]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_JOIN_GUILD)
        
        if joinGuildType == 0:
            try:
                guildMemberDB = DB.dbConnection.customeSelectListQuery("SELECT A.iAccountId, A.cName, A.iLevel, A.iExp, A.iportrait, A.ibestRecord, A.iwinRecord, A.icontinueRecord \
                FROM gamedb.guild_member AS M \
                LEFT JOIN gamedb.account AS A \
                ON M.iAccountId = A.iAccountId WHERE iguildIdx = %s" % joinGuildIdx)
             
                guildInfoDB = DB.dbConnection.customSelectQuery("select * from gamedb.guild where iguildIdx = %s" % joinGuildIdx)
            except Exception as e:
                print(str(e))
                return Common.respHandler.getResponse(Route.Define.ERROR_DB)
            
            guildContainer = userObject.getData(Define.GUILD_CONTANIER)
            guildContainer.loadBasicInitDataFromDB(guildInfoDB)
            guildContainer.loadValueFromDB(guildMemberDB)
            Common.respHandler.mergeResp(guildContainer.getContainerResp())
                           
        #TODO : Redis Pub sub -> Gameserver -> User packet
        return Common.respHandler.getResponse(Route.Define.OK_JOIN_SIGN_UP)    


class GuildLeave(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        #checkGuildExist
        userObject = userCachedObjects[session]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        guildContainer = userObject.getData(Define.GUILD_CONTANIER)
        
        if guildContainer.idx is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_JOIN_GUILD))
        
        guildGrade = 0
        if accountInfo.accountId == guildContainer.leaderId:
            guildGrade = 2
        elif accountInfo.accountId == guildContainer.leaderId2:
            guildGrade = 1
        
        o_error = 0
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Guild_Leave", (accountInfo.accountId, guildContainer.idx, guildGrade, o_error), (3, 3))
        except Exception as e:
            print(str(e))
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        o_error = resultDB[1]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_JOIN_GUILD)
        
        if guildContainer.leaderId == accountInfo.accountId:
            #TODO : Redis pub sub -> Gameserve -> user Packet send
            send = 0
            
        #TODO : Redis pub sub -> Expire guild join check
            
        guildContainer.resetGuildInfo()        
        return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)      
    
    
class GuildKick(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("kickUserId")
        args = Route.parser.parse_args()
        
        if not args["kickUserId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        #checkGuildExist
        userObject = userCachedObjects[session]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        guildContainer = userObject.getData(Define.GUILD_CONTANIER) 
        
        if guildContainer.idx is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_JOIN_GUILD))
        
        if guildContainer.leaderId != accountInfo.accountId and guildContainer.leaderId2 != accountInfo.accountId:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INVALID_ACCESS))
        
        kickUserId = int(args["kickUserId"])
        
        if guildContainer.leaderId == kickUserId or accountInfo.accountId == kickUserId:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        guildGrade = 0 if kickUserId != guildContainer.leaderId2 else 1
                
        o_error = 0
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Guild_Kick", (accountInfo.accountId, guildContainer.idx, kickUserId, guildGrade, o_error), (4, 4))
        except Exception as e:
            print(str(e))
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        o_error = resultDB[1]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        #mail send..
        o_error = 0
#         try:
#             resultDB = DB.dbConnection.executeStoredProcedure("Game_Mail_Write", (targetNickName, accountInfo.accountId, title, body, accountInfo.gameMoney, o_error), (5, 5))
#         except Exception as e:
#             print(str(e))
#             return Route.Define.ERROR_DB
        
        if guildGrade == 1:
            guildContainer.leaderId2 = 0
            
        return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)     