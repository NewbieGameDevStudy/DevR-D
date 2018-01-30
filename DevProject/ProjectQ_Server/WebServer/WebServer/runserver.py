"""
This script runs the WebServer application using a development server.
"""

from WebServer import app

#print(environ.get('SERVER_HOST', 'localhost'))
#print(environ.get('SERVER_PORT', '5555'))

"""
server host
server port

DB host
DB port

DB root
DB password

"""

#if __name__ == "__main__":
#    app.run(debug = __debug__)

"""
if __name__ == '__main__':
    HOST = environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(environ.get('SERVER_PORT', '5555'))
    except method_name():
        PORT = 5555
    app.run(HOST, PORT)"""