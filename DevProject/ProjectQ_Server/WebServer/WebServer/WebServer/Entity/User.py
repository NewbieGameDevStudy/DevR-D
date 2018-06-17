'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import ACCOUNT_INFO, ITEM_CONTANIER, MAIL_CONTANIER, GUILD_CONTANIER
from Entity import Container, Account, Guild
from Route import Common

class UserObject(object):
    def __init__(self):
        self.cachedDict = {}
        self.accountInfo = Account.Account()
        self.itemContainer = Container.ItemContainer()
        self.mailContainer = Container.MailContainer()
        self.guildConatiner = Container.GuildContainer()
        
        #cached init dict
        self.initCachedDict()
    
    def initCachedDict(self):
        self.cachedDict[ACCOUNT_INFO] = self.accountInfo
        self.cachedDict[ITEM_CONTANIER] = self.itemContainer
        self.cachedDict[MAIL_CONTANIER] = self.mailContainer
        self.cachedDict[GUILD_CONTANIER] = self.guildConatiner 
    
    def getData(self, dataType):
        if dataType in self.cachedDict:
            return self.cachedDict[dataType]
        return None
    
        
class UserInfo(Common.BaseObjResp):
    def __init__(self, accountInfo):
        super(UserInfo, self).__init__()
        self.accountId = accountInfo.accountId
        self.name = accountInfo.name
        self.level = accountInfo.level
        self.exp = accountInfo.exp
        self.portrait = accountInfo.portrait
        
        self.initRespCache()    

    def getResp(self):
        return self.ig_respDict