"""
This script runs the WebServer application using a development server.
"""

from WebServer import app
from WebServer.config import SERVER_VALUE


#from WebServer.API.database import exe_query
from WebServer.API.database import DBManager

sql_data = DBManager()
sql_data.execute_query("show databases")
print(sql_data.data)

if __name__ == '__main__':
    app.run(host = SERVER_VALUE.SERVER_HOST, port = SERVER_VALUE.SERVER_PORT, debug = True)

"""
if __name__ == '__main__':
    HOST = 'localhost'#environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = 5000 #int(environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
"""

