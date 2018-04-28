from flask_restful import Resource
from flask import jsonify, request, session

import DB
import Route  

class RespPlayerInfo(object):
 
    def setInfo(self, result, level, exp, name, gameMoney):
        self.result = result
        self.level = level
        self.exp = exp
        self.gameMoney = gameMoney
        self.name = name
  
    def serialize(self):
        return {
            'result':self.result,
            'level':self.level,
            'exp':self.exp,
            'gameMoney':self.gameMoney,
            'name':self.name,
        }
  
class Login(Resource):
 
    def get(self):
        Route.parser.add_argument('accountId')
        args = Route.parser.parse_args()
        accountId = args["accountId"]
        result = DB.dbConnection.SelectQuery("gamedb.playerinfo", "uAccountId", str(accountId), "iLevel", "iExp", "cName", "iGameMoney")
  
        if 'a' in session:
            sessionKey = session['name']
            print(sessionKey)
            cookie = request.cookies.get('name')
            
        d = 0
        session['name'] = id
        s = session['name']
        playerInfo = RespPlayerInfo()    
        if result is None:
            playerInfo.result = False
        else:
            playerInfo.setInfo(True, result[0], result[1], result[2], result[3])
  
        return jsonify(playerInfo.serialize())

    def post(self):
        Route.parser.add_argument('accountId', type=int)
        args = Route.parser.parse_args()
        
        
        # id = args["accountId"]
        # query = 'select NVL(level, -1), NVL(exp, -1) from playerinfo where uAccountId = %d' % id
        # result = dbConnection.select_query(query)
        return jsonify(1)

