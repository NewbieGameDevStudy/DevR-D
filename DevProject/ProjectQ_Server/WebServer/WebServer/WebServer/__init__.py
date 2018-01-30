"""
The flask application package.
"""

from flask import Flask
from WebServer.API import *

from WebServer import config
from WebServer import database

app = Flask(__name__)



#import WebServer.views
