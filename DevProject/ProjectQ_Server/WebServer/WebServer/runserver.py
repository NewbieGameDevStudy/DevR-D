"""
This script runs the WebServer application using a development server.
"""

from WebServer import app
from WebServer.config import SERVER_VALUE



if __name__ == '__main__':
    app.run(host = SERVER_VALUE.SERVER_HOST, port = SERVER_VALUE.SERVER_PORT, debug = True)

"""
if __name__ == '__main__':
    HOST = environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
"""