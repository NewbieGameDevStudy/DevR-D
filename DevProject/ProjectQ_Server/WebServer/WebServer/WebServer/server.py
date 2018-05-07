import Route
import os
import Util

from flask import Flask
from flask_restful import Api

if __name__ == '__main__':
    app = Flask(__name__)
    api = Api(app)
    
    secret_ket = os.urandom(24)
    app.secret_key = secret_ket
    
    #setting
    Util.guidInst.setGuid(1)
       
    for routeClass, routeString in Route.route_dict.items():
        api.add_resource(routeClass, routeString)
    
    app.run('localhost', 5000)