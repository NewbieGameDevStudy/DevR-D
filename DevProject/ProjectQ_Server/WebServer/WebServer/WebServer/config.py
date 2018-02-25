import os
import configparser

#string append function
def str_join(*args):
    return ''.join(map(str, args))

#set environment from .ini file
config = configparser.ConfigParser()
config.read(str_join(os.getcwd(), '\\', "config.ini"))

if __debug__ == True:
    str_states = 'TEST'
else:
    str_states = 'DEFAULT'

#WebServer envifonment Values
class SERVER_VALUE:
    SERVER_HOST = config[str_states]['SERVER_HOST']
    SERVER_PORT = int(config[str_states]['SERVER_PORT'])


class MYSQL:
    DB_CONNECT = config[str_states]['DB_CONNECT']
    DB_USER = config[str_states]['DB_USER']
    DB_PASSWORD = config[str_states]['DB_PASSWORD']
    DB_PORT = config[str_states]['DB_PORT']
