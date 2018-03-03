from flask import Flask
from flask_restful import Api
from flask_restful import reqparse, request
from .Database.DataBase import DBConnection

app = Flask(__name__)
api = Api(app)

dbConnection = DBConnection()

from .Player import PlayerInfo

