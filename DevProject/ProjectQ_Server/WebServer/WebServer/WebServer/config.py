"""
WebServer config setting here
"""

import os
import configparser

#string append function
def str_join(*args):
    return ''.join(map(str, args))



#set environment from .ini file
config = configparser.ConfigParser()

"""
path_config = os.getcwd()
config_filename = "config.ini"
path_config = str_join(path_config, '\\', config_filename)
config.read(path_config)
"""
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
    DB_NAME = config[str_states]['DB_NAME']


"""
DB_CONNECT = "localhost"
DB_USER = "test"
DB_PASSWORD = "projectq1234"
DB_NAME = "project_q"
"""