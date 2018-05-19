'''
Created on 2018. 5. 13.

@author: namoeye
'''
from DB.TableData import PlayerInfo
from Entity.Define import PLAYER_INFO

class UserObject(object):
    def __init__(self):
        self.cachedDict = {}
        self.playerInfo = PlayerInfo()
        
        
        #cached init dict
        self.initCachedDict()
    
    def initCachedDict(self):
        self.cachedDict[PLAYER_INFO] = self.playerInfo
    
    def getData(self, dataType):
        if PLAYER_INFO in self.cachedDict:
            return self.cachedDict[PLAYER_INFO]