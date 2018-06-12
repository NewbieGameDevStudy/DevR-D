from Route import Common
from datetime import datetime

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
        self.guildCreateTime = convertList[9]
        
        self.initResp(convertList)
        
    def getResp(self):
        return {self.__class__.__name__ : self.ig_respDict}
    
    def CreateGuild(self, guildIdx, guildName, guildJoinType, guildMark, leaderAccountId):
        self.guildIdx = guildIdx
        self.guildName = guildName
        self.guildJoinType = guildJoinType
        self.guildLeaderId = leaderAccountId
        self.guildMark = guildMark
        self.guildCreateTime = int(datetime.now().timestamp())
        