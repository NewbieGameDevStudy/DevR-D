"""
The flask application package.
"""

from flask import Flask


app = Flask(__name__)

from WebServer.API import database
from WebServer.API import *

#import WebServer.views
