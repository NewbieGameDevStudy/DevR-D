from Route.Login import Login
from Route import Login
from flask_restful import reqparse
from Route.Shop import TradeShop

route_dict = { 
    Login:'/loginInfo', 
    TradeShop:'/tradeShop' 
}

parser = reqparse.RequestParser()