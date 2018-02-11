import MySQLdb
from flask import jsonify
from contextlib import contextmanager
import json
from WebServer.config import MYSQL


class DBManager:
    def __init__(self):
        self.con = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
        self.cursor = self.con.cursor()
        self.data = None

    @contextmanager
    def database(self, str):
        try:
            con = self.con
            cursor = self.cursor           
            yield (con, cursor)
        finally:
            self.cursor.close()
            self.con.close()

    def execute_query(self, str):
        with self.database(str) as (con, cursor):
             cursor.execute(str)
             self.data = cursor.fetchall()
        #return jsonify(self.data)
        return json.dumps(self.data)

    def select_query(self, str):
        with self.database(str) as (con, cursor):
             cursor.execute(str)
             con.commit()

"""
@contextmanager
def execute_query(str_query):
    if str_query is None:
        return None

    db = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
    cursor = db.cursor()

    try:
        cursor.execute(str_query)
        yield (db, cursor)
    finally:
        cursor.close()
        db.close()

def exe_query(str_q):
    data = None

    with execute_query(str_q) as (db, cursor):
        db.commit()
        data = cursor.fetchall()

    #return json.dumps(data)
    return jsonify(data)
"""


print("database")
#db = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
#db = MySQLdb.connect("127.0.0.1:3306", "test", "projectq1234", "project_q")
#cursor = db.cursor()

#Need add DB exception
"""
def execute_query(str_query):
    cursor.execute(str_query)
    data = cursor.fetchall()
    return json.dumps(data)
"""
        