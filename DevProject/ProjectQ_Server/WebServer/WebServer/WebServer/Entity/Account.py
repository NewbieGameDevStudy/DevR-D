from Route import Common

class Account(Common.BaseObjResp):
    def __init__(self):
        super(Account, self).__init__()
        self.accountId = 0
        self.name = ''
        self.level = 1
        self.exp = 0
        self.gameMoney = 0
        self.portrait = 0
        self.bestRecord = 0
        self.winRecord = 0
        self.continueRecord = 0
        self.dailyMailCount = 0
        
        self.initRespCache()
    
    def loadValueFromDB(self, updateList):
        convertList = list(updateList)
        self.accountId = convertList[0]
        self.name = convertList[1]
        self.level = convertList[2]
        self.exp = convertList[3]
        self.gameMoney = convertList[4]
        self.portrait = convertList[5]
        self.bestRecord = convertList[6]
        self.winRecord = convertList[7]
        self.continueRecord = convertList[8]
        self.continueRecord = convertList[9]
        
        self.initResp(convertList)
        
    def getResp(self):
        return {self.__class__.__name__ : self.ig_respDict}