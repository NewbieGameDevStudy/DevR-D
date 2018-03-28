import Route

from flask import Flask
from flask_restful import Api

if __name__ == '__main__':
    app = Flask(__name__)
    api = Api(app)
    
    for routeClass, routeString in Route.route_dict.items():
        api.add_resource(routeClass, routeString)
    
    app.run('localhost', 5000)