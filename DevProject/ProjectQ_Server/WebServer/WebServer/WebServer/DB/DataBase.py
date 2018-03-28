import MySQLdb
from contextlib import contextmanager

class DBConnection:
    
    def __init__(self):
        self.con = MySQLdb.connect(host="localhost", user="root", passwd="1234567890")
    
    @contextmanager
    def database(self):
        try:
            self.cur = self.con.cursor()
            yield (self.cur)
        finally:
            self.cur.close()
            
    def select_query(self, queryStr):
        with self.database() as (cur):
            cur.execute(queryStr)
            result = cur.fetchone()
        return result

    def execute_query(self, queryStr):
        with self.database() as (con, cursor):
            cursor.execute(queryStr)
            con.commit()
