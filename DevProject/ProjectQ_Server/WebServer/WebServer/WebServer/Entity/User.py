'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import ACCOUNT_INFO, ITEM_CONTANIER, MAIL_CONTANIER
from Entity.Item import Item
from Entity.Mail import Mail
from Route import Common
from Route import Define
from Route.Define import OK_EQUIP_ITEM

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
    def __init__(self):
        super(ItemContainer, self).__init__()
        self.inventory = [0, 0]        
        self.initRespCache()
    
    def loadBasicInitDataFromDB(self, updateList):        
        if len(updateList) == 0:
            return
        
        #dbTable == data 1:1 match
        self.inventory[0] = updateList[0]
        self.inventory[1] = updateList[1]
        
        self.syncToResp()
    
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            dataIdx = datas[0]
            if not dataIdx in self.ig_container:
                self.ig_container[dataIdx] = Item()
            
            item = self.ig_container[dataIdx]
            item.loadValueFromDB(datas[0], datas[2], datas[3])
            
            if item.itemIdx in self.inventory:
                item.equip = 1
            
            item.syncToResp()
            
    def getItemById(self, itemId):
        for item in self.ig_container.values():
            if item.itemId == itemId:
                return item
        return None
    
    def getItemByIdx(self, itemIdx):
        for item in self.ig_container.values():
            if item.itemIdx == itemIdx:
                return item
        return None 
        
    def EquipItem(self, slotId, itemIdx):
        if slotId > len(self.inventory) - 1:
            return Define.ERROR_OUT_OF_RANGE
        
        equipItem = self.getItemByIdx(itemIdx)
        if equipItem is None:
            return Define.ERROR_NOT_FOUND_ITEM
        
        prevIdx = self.inventory[slotId]
        
        if prevIdx == itemIdx or equipItem.equip == 1:
            return Define.ERROR_ALREADY_EQUIP_ITEM
        
        self.inventory[slotId] = itemIdx        
        
        if not prevIdx is 0:
            prevItem = self.getItemByIdx(prevIdx)
            prevItem.equip = 0
        
        equipItem.equip = 1
        return OK_EQUIP_ITEM
    
    def UnEquipItem(self, slotId, itemIdx):
        if slotId > len(self.inventory) - 1:
            return Define.ERROR_OUT_OF_RANGE
        
        unEquipItemIdx = self.inventory[slotId]
        if unEquipItemIdx == 0 or unEquipItemIdx != itemIdx:
            return Define.ERROR_NOT_FOUND_ITEM
        
        equipItem = self.getItemByIdx(itemIdx)
        equipItem.equip = 0
        self.inventory[slotId] = 0
        
        return Define.OK_UNEQUIP_ITEM      

class MailContainer(Common.BaseContainerResp):
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            datas = list(datas)
            dataIdx = datas[0]
            if not dataIdx in self.ig_container:
                self.ig_container[dataIdx] = Mail()
            
            del datas[2]
            mail = self.ig_container[dataIdx]
            mail.loadValueFromDB(datas)
            mail.syncToResp()
            
    def getItemById(self, mailIdx):
        for mail in self.ig_container.values():
            if mail.mailIdx == mailIdx:
                return mail
            
        return None