import MySQLdb
import json
from contextlib import contextmanager
from .config import MYSQL

class DBConnection:
    def __init__(self):
        pass

    def cursor(self):
        return self.cursor

    @contextmanager
    def database(self, str):
        try:
            self.con = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_PORT, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
            self.cursor = self.con.cursor()
            con = self.con
            cursor = self.cursor
            yield (con, cursor)
        finally:
            self.cursor.close()
            self.con.close()

    def select_query(self, str):
        with self.database(str) as (con, cursor):
             cursor.execute(str)
        
        if cursor.fetchone() :
            return json.dumps(cursor.fetchall())
        else:
            return None

    def execute_query(self, str):
        with self.database(str) as (con, cursor):
             cursor.execute(str)
             con.commit()

dbConnection = DBConnection()

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
        