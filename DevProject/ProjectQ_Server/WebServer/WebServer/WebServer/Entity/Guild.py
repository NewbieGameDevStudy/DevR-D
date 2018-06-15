from Route import Common
from datetime import datetime

class GuildMemberInfo(Common.BaseObjResp):
    def __init__(self):
        super(Guild, self).__init__()
        self.accountId = 0
        self.name = ""
        self.level = 0
        self.exp = 0
        self.portrait = 0
        self.bestRecord = 0
        self.winRecord = 0
        self.continueRecord = 0
        self.memberGrade = 0
        
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
        self.memberGrade = updateList[8]
        
        self.initResp(updateList)
        

class Guild(Common.BaseObjResp):
    def __init__(self):
        super(Guild, self).__init__()
        self.guildIdx = 0
        self.guildName = ''
        self.guildMemberCount = 1
        self.guildJoinType = 0
        self.guildLeaderId = 0
        #second guild leader
        self.guildLeaderId2 = 0     
        self.guildGrade = 0
        self.guildMark = 0
        self.guildScore = 0
        self.guildCreateTime = 0
        
        self.initRespCache()
    
    def loadValueFromDB(self, updateList):
        convertList = updateList
        self.guildIdx = convertList[0]
        self.guildName = convertList[1]
        self.guildMemberCount = convertList[2]
        self.guildJoinType = convertList[3]
        self.guildLeaderId = convertList[4]
        self.guildLeaderId2 = convertList[5]
        self.guildGrade = convertList[6]
        self.guildMark = convertList[7]
        self.guildScore = convertList[8]
        self.guildCreateTime = int(convertList[9].timestamp()) 
        
        self.initResp(convertList)
        
    def getResp(self):
        return {self.__class__.__name__ : self.ig_respDict}
    
    def updateGuild(self, guildIdx, guildName, guildJoinType, guildMark, leaderAccountId):
        self.guildIdx = guildIdx
        self.guildName = guildName
        self.guildJoinType = guildJoinType
        self.guildLeaderId = leaderAccountId
        self.guildMark = guildMark
        self.guildCreateTime = int(datetime.now().timestamp())
    
    def resetGuildInfo(self):
        self.guildIdx = 0
        self.guildName = ''
        self.guildMemberCount = 1
        self.guildJoinType = 0
        self.guildLeaderId = 0
        #second guild leader
        self.guildLeaderId2 = 0     
        self.guildGrade = 0
        self.guildMark = 0
        self.guildScore = 0
        self.guildCreateTime = 0
        
        self.syncToResp()
        