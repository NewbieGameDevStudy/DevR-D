# coding=utf8

from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
import json
from Route import Common
from Entity import userCachedObjects, Define
from Entity import User

class UserFind(Resource, Common.BaseRoute):
    def post(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("nickName")
        args = Route.parser.parse_args()
        
        if not args["nickName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        nickname = "\"%s\"" % args["nickName"] 
                
        try:
            resultDB = DB.dbConnection.customSelectQuery("select cname from gamedb.account where cname = %s" % nickname)
            if resultDB is None or len(resultDB) == 0:
                return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_USER))
        except Exception as e:
            print(str(e))
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_DB))
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"nickName" : resultDB[0]}))


class UserInfo(Resource, Common.BaseRoute):
    def get(self):
        Route.parser.add_argument("accountId")
        args = Route.parser.parse_args()
        
        if not args["accountId"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))

        accountId = args["accountId"]
        
        if not accountId in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        userObject = userCachedObjects[accountId]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        userInfo = User.UserInfo(accountInfo)
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"userInfo":userInfo.getResp()}))
    
    
class UserInfos(Resource, Common.BaseRoute):
    def get(self):       
        Route.parser.add_argument("accountIds", location=json)
        args = Route.parser.parse_args()
        if not args["accountIds"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        accountIds = args["accountIds"]
        
        userInfos = {"userInfos":[]}
        for id in accountIds:
            if not id in userCachedObjects:
                print("not found user : %s" % id)
                continue
            
            accountInfo = userCachedObjects[id].getData(Define.ACCOUNT_INFO)
            userInfo = UserInfo(accountInfo)
            userInfos["userInfos"].append(userInfo.getResp())
                                                                
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, userInfos))