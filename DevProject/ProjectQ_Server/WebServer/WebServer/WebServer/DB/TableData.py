'''
Created on 2018. 5. 13.

@author: namoeye
'''

from Route import Common

class PlayerInfo(Common.ObjRespBase):
    def __init__(self):
        super(PlayerInfo, self).__init__()
        self.db_accountId = 0
        self.db_level = 1
        self.db_exp = 0
        self.db_gameMoney = 0
        self.db_name = ''
        self.db_portrait = 0
        self.db_bestRecord = 0
        self.db_winRecord = 0
        self.db_continueRecord = 0
        
        super(PlayerInfo, self).initFieldDBQueryCache()