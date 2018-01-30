"""
DB-Setting here
"""

import MySQLdb
import WebServer.config as database_path

db = MySQLdb.connect("localhost", "user", "password", "DB")



