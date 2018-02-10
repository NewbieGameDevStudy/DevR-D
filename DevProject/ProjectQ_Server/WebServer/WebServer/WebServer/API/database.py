import MySQLdb
import json
from WebServer.config import MYSQL

print("database")
db = MySQLdb.connect(MYSQL.DB_CONNECT, MYSQL.DB_USER, MYSQL.DB_PASSWORD, MYSQL.DB_NAME)
#db = MySQLdb.connect("127.0.0.1:3306", "test", "projectq1234", "project_q")
cursor = db.cursor()



#Need add DB exception

def execute_query(str_query):
    cursor.execute(str_query)
    data = cursor.fetchall()
    return json.dumps(data)

        