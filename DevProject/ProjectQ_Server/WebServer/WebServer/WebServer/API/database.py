import MySQLdb
import json
from WebServer.config import MYSQL


db = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
cursor = db.cursor()



#Need add DB exception

def execute_query(str_query):
    cursor.execute(str_query)
    data = cursor.fetchall()
    return json.dumps(data)

        