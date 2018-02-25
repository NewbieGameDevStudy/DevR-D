from RestApi import *
from config import *
from config import SERVER_VALUE

from flask_restful import Resource, Api, fields, marshal_with
from flask_restful import reqparse, request
from flask import Flask, jsonify
import MySQLdb
import json

if __name__ == '__main__':
    app.run(host = SERVER_VALUE.SERVER_HOST, port = SERVER_VALUE.SERVER_PORT)