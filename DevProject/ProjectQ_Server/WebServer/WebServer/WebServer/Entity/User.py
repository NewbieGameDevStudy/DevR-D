'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import ACCOUNT_INFO, ITEM_CONTANIER, MAIL_CONTANIER
from Entity.Item import Item
from Entity.Mail import Mail
from Route import Common

class UserObject(object):
    def __init__(self):
        self.cachedDict = {}
        self.accountInfo = Account()
        self.itemContainer = ItemContainer()
        self.mailContainer = MailContainer()
        
        #cached init dict
        self.initCachedDict()
    
    def initCachedDict(self):
        self.cachedDict[ACCOUNT_INFO] = self.accountInfo
        self.cachedDict[ITEM_CONTANIER] = self.itemContainer
        self.cachedDict[MAIL_CONTANIER] = self.mailContainer
    
    def getData(self, dataType):
        if dataType in self.cachedDict:
            return self.cachedDict[dataType]
        return None
    
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
        
        self.initResp(convertList)
        
    def getResp(self):
        return {self.__class__.__name__ : self.ig_respDict}
            

class ItemContainer(Common.BaseContainerResp):
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            dataIdx = datas[0]
            if not 0 in self.container:
                self.container[dataIdx] = Item()
            
            item = self.container[dataIdx]
            item.loadValueFromDB(datas[0], datas[2])
            #item.initResp(datas)
            item.syncToResp()
            
    def getItemById(self, itemId):
        for item in self.container.values():
            if item.itemId == itemId:
                return item
            
        return None 
    

class MailContainer(Common.BaseContainerResp):
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            datas = list(datas)
            dataIdx = datas[0]
            if not 0 in self.container:
                self.container[dataIdx] = Mail()
            
            del datas[2]
            mail = self.container[dataIdx]
            mail.loadValueFromDB(datas)
            mail.syncToResp()
            
    def getItemById(self, mailIdx):
        for mail in self.container.values():
            if mail.mailIdx == mailIdx:
                return mail
            
        return None