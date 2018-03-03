import MySQLdb
import json
from contextlib import contextmanager
#from config import MYSQL

class DBConnection:
    @contextmanager
    def database(self):
        try:
            #self.con = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD)
            self.con = MySQLdb.connect(host="localhost", user="root", passwd="1234567890")
            self.cur = self.con.cursor()
            yield (self.cur)
        finally:
            self.cur.close()
            self.con.close()

    def select_query(self, str):
        with self.database() as (cur):
             cur.execute(str)
             result = cur.fetchone()
        return result

    def execute_query(self, str):
        with self.database() as (con, cursor):
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
        