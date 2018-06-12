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
        
        guildInfo.CreateGuild(o_guildIdx, createGuildName, guildJoinType, guildMark, accountInfo.accountId)
        guildInfo.syncToResp()
        
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, guildInfo.getResp())        
        
        
class GuildJoin(Resource, Common.BaseRoute):
    def post(self):
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
        
        o_error = resultDB[0]
        
        if o_error == -1:
            return Common.respHandler.getResponse(Route.Define.ERROR_NOT_WRITE)
                
        return Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"mailCount" : accountInfo.dailyMailCount, "gameMoney" : accountInfo.gameMoney})    
        