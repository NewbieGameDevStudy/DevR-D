"""
file log of webserver
"""

import logging

logging.basicConfig(filename = './log/auth.log', 
                    filemode = 'a', 
                    level = logging.DEBUG,
                    format = '[%(asctime)s][%(levelname)s] %(message)s')


