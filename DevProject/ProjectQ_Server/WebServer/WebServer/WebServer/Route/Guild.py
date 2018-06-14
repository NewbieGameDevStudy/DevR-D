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
        guildInfo = userObject.getData(Define.GUILD_INFO)
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if guildInfo.guildIdx > 0:
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
        
        guildInfo.updateGuild(o_guildIdx, createGuildName, guildJoinType, guildMark, accountInfo.accountId)
        guildInfo.syncToResp()
        
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, guildInfo.getResp())        
        
        
class GuildJoin(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("guildName")
        args = Route.parser.parse_args()
        
        if not args["guildName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))

        #checkGuildJoin
        joinGuildName = args["guildName"]
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
        
        o_error = resultDB[1]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_JOIN_GUILD)
        
        if joinGuildType == 0:
            guildInfo = userObject.getData(Define.GUILD_INFO)        
            guildInfo.loadValueFromDB(resultDB[0])
            guildInfo.syncToResp()
            Common.respHandler.mergeResp(guildInfo.getResp())
                        
        #TODO : Redis Pub sub -> Gameserver -> User packet
        return Common.respHandler.getResponse(Route.Define.OK_JOIN_SIGN_UP)    


class GuildExit(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        #checkGuildExist
        userObject = userCachedObjects[session]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        guildInfo = userObject.getData(Define.GUILD_INFO)    
        
        if guildInfo.guildIdx is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_JOIN_GUILD))
        
        o_error = 0
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Guild_Exit", (accountInfo.accountId, guildInfo.guildIdx, o_error), (2, 2))
        except Exception as e:
            print(str(e))
            return Common.respHandler.getResponse(Route.Define.ERROR_DB)
        
        o_error = resultDB[1]
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_JOIN_GUILD)
        
        if guildInfo.guildLeaderId == accountInfo.accountId:
            #TODO : Redis pub sub -> Gameserve -> user Packet send
            send = 0
            
        #TODO : Redis pub sub -> Expire guild join check
            
        guildInfo.resetGuildInfo()        
        return Common.respHandler.getResponse(Route.Define.OK_SUCCESS)        