'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import ACCOUNT_INFO, ITEM_CONTANIER, MAIL_CONTANIER, GUILD_CONTANIER

from Entity import Container, Account, Guild

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
    

            

