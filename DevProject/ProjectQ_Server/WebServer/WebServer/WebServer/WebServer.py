#from config import SERVER_VALUE
import RestApi

from flask_restful import Resource, Api, fields, marshal_with
from flask_restful import reqparse, request
from flask import Flask, jsonify
import MySQLdb
import json

if __name__ == '__main__':
    #RestApi.app.run(host = SERVER_VALUE.SERVER_HOST, port = SERVER_VALUE.SERVER_PORT)
    RestApi.app.run('localhost', 5000)