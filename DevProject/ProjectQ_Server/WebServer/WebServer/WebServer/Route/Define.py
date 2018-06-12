'''
Created on 2018. 4. 29.

@author: namoeye
'''

#Error Code
ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101
ERROR_NOT_FOUND_SESSION = 103
ERROR_INPUT_PARAMS = 102
ERROR_INVALID_ACCESS = 104
ERROR_OUT_OF_RANGE = 105
ERROR_DB = 106
ERROR_NOT_FOUND = 107

#Login Create
ERROR_CREATE_NOT_LOGIN = 1001
ERROR_ALREADY_CREATE_NICKNAME = 1002
OK_CREATE_LOGIN = 2001

#Success
OK_LOGIN_CONNECT = 2002
OK_SUCCESS = 2003

#Shop
ERROR_INVALID_BUY_PRODUCT = 3001
ERROR_NOT_ENOUGH_MONEY = 3002
ERROR_NOT_FOUND_ITEM = 3003
OK_SHOP_BUY_PRODUCT = 3004
ERROR_ALREADY_BUY_NO_STOCK_ITEM = 3005
ERROR_REQUEST_SINGLE_ITEM = 3006

#Inventory
ERROR_ALREADY_EQUIP_ITEM = 4001
OK_EQUIP_ITEM = 4002
OK_UNEQUIP_ITEM = 4003

#User
ERROR_NOT_FOUND_USER = 5001

#Mail
ERROR_NOT_WRITE = 6001
ERROR_ALREADY_READ_DONE = 6002


#Guild
ERROR_CURRENT_JOIN_GUILD = 7001
ERROR_LOW_LEVEL = 7002
ERROR_ALREADY_GUILDNAME = 7003
ERROR_NOT_CREATE_GUILD = 7004;