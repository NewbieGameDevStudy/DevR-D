'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import ACCOUNT_INFO, ITEM_CONTANIER, MAIL_CONTANIER, GUILD_INFO

from Entity import Container
from Entity import Account
from Entity import Guild

class UserObject(object):
    def __init__(self):
        self.cachedDict = {}
        self.accountInfo = Account.Account()
        self.itemContainer = Container.ItemContainer()
        self.mailContainer = Container.MailContainer()
        self.guildInfo = Guild.Guild()
        
        #cached init dict
        self.initCachedDict()
    
    def initCachedDict(self):
        self.cachedDict[ACCOUNT_INFO] = self.accountInfo
        self.cachedDict[ITEM_CONTANIER] = self.itemContainer
        self.cachedDict[MAIL_CONTANIER] = self.mailContainer
        self.cachedDict[GUILD_INFO] = self.guildInfo 
    
    def getData(self, dataType):
        if dataType in self.cachedDict:
            return self.cachedDict[dataType]
        return None
    

            

