import Route
import os
import Util

from flask import Flask
from flask_restful import Api
from Entity.Define import SHOP
from Entity import Shop
import Entity

def serverInit():
    Entity.serverCachedObject[Entity.Define.SHOP] = Shop.ShopBase()

if __name__ == '__main__':
    app = Flask(__name__)
    api = Api(app)

    secret_ket = os.urandom(24)
    app.secret_key = secret_ket

    #setting
    Util.guidInst.setGuid(1)
    serverInit()

    for routeClass, routeString in Route.route_dict.items():
        api.add_resource(routeClass, routeString)
    
    app.run('127.0.0.1', 5000)
    
    
