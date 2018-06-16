from Route import Common
from datetime import datetime

class GuildMemberInfo(Common.BaseObjResp):
    def __init__(self):
        super(GuildMemberInfo, self).__init__()
        self.accountId = 0
        self.name = ""
        self.level = 0
        self.exp = 0
        self.portrait = 0
        self.bestRecord = 0
        self.winRecord = 0
        self.continueRecord = 0
        
        self.initRespCache()
        
    def loadValueFromDB(self, updateList):
        self.accountId = updateList[0]
        self.name = updateList[1]
        self.level = updateList[2]
        self.exp = updateList[3]
        self.portrait = updateList[4]
        self.bestRecord = updateList[5]
        self.winRecord = updateList[6]
        self.continueRecord = updateList[7]
        
        self.initResp(updateList)
        
    def getResp(self):
        return self.ig_respDict
        