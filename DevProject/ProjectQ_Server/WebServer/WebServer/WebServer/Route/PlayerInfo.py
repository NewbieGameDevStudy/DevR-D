from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route.Common
from random import randint

class RespPlayerInfo(Route.Common.RespBase):
    def __init__(self):
        Route.Common.RespBase.__init__(self)
        self.level = 0
        self.exp = 0
        self.gameMoney = 0
        self.name = ''
  
class Login(Resource):
    def get(self):
        Route.parser.add_argument('accountId')
        args = Route.parser.parse_args()
        
        if not 'accountId' in args:
            return #여기에 에러코드를 포함하면됨
            
        accountId = args['accountId']
        playerInfo = RespPlayerInfo()   
        result = DB.dbConnection.selectQuery("gamedb.account", "uAccount", str(accountId), playerInfo.convertDBField())
        if not result:
            return jsonify(playerInfo.errorToJson(Route.Define.ERROR_LOGIN_NOT_FOUND_ACCOUNT))
        
        if 'name' in session:
            sessionKey = session['name']
            print(sessionKey)
            cookie = request.cookies.get('name')
            
        session['name'] = 10
        s = session['name']
        
        return jsonify(playerInfo.successToJson(result))
  
    def post(self):
        Route.parser.add_argument('accountId', type=int)
        args = Route.parser.parse_args()
        
        
        # id = args["accountId"]
        # query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        # result = dbConnection.select_query(query)
        return jsonify(1)

