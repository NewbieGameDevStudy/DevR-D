from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
from Route import Common
from Entity import userCachedObjects

class UserFind(Resource, Common.BaseRoute):
    def get(self):
        session = self.getSession(request)
        if session is None:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        if not session in userCachedObjects:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_SESSION))
        
        Route.parser.add_argument("nickName")
        args = Route.parser.parse_args()
        
        if not args["nickName"]:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_INPUT_PARAMS))
        
        nickName = args["nickName"]
                
        resultDB = DB.dbConnection.customSelectQuery("select cname from gamedb.account where cname = \"%s\"" % nickName)
        if resultDB is None or len(resultDB) == 0:
            return jsonify(Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_USER))
        
        return jsonify(Common.respHandler.customeResponse(Route.Define.OK_SUCCESS, {"nickName" : resultDB[0]}))
