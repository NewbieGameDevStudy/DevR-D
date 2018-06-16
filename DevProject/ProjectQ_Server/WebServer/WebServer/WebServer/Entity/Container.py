'''
Created on 2018. 6. 3.

@author: namoeye
'''

from Route import Common, Define
from Entity.Item import Item
from Entity.Mail import Mail
from Entity.Guild import GuildMemberInfo
from datetime import datetime

class GuildContainer(Common.BaseContainerResp):
    def __init__(self):
        super(GuildContainer, self).__init__()
        self.idx = 0
        self.name = ''
        self.memberCount = 1
        self.joinType = 0
        self.leaderId = 0
        self.leaderId2 = 0     
        self.grade = 0
        self.mark = 0
        self.score = 0
        self.createTime = 0
        
        self.initRespCache()
        
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            dataIdx = datas[0]
            if not dataIdx in self.ig_container:
                self.ig_container[dataIdx] = GuildMemberInfo()
            
            memberInfo = self.ig_container[dataIdx]
            memberInfo.loadValueFromDB(datas)            
            memberInfo.syncToResp()
    
    def loadBasicInitDataFromDB(self, updateList):
        if updateList is None or len(updateList) == 0:
            return
        
        #dbTable == data 1:1 match
        self.idx = updateList[0]
        self.name = updateList[1]
        self.memberCount = updateList[2]
        self.joinType = updateList[3]
        self.leaderId = updateList[4]
        self.leaderId2 = updateList[5]
        self.grade = updateList[6]
        self.mark = updateList[7]
        self.score = updateList[8]
        self.createTime = int(updateList[9].timestamp()) 
        
        self.syncToResp()
        
    def updateGuild(self, guildIdx, guildName, guildJoinType, guildMark, leaderAccountId):
        self.idx = guildIdx
        self.name = guildName
        self.joinType = guildJoinType
        self.leaderId = leaderAccountId
        self.mark = guildMark
        self.createTime = int(datetime.now().timestamp())
    
    def resetGuildInfo(self):
        self.idx = 0
        self.name = ''
        self.memberCount = 1
        self.joinType = 0
        self.leaderId = 0        
        self.leaderId2 = 0     
        self.grade = 0
        self.mark = 0
        self.score = 0
        self.createTime = 0
        
        self.ig_container.clear()
        self.syncToResp()
        
    def setGuildMemberInfo(self, memberInfo):
        if memberInfo.accountId in self.ig_container:
            del self.ig_container[memberInfo.accountId]            
                    
        self.ig_container[memberInfo.accountId] = memberInfo        

class ItemContainer(Common.BaseContainerResp):
    def __init__(self):
        super(ItemContainer, self).__init__()
        self.slot = [0, 0]        
        self.initRespCache()
    
    def loadBasicInitDataFromDB(self, updateList):
        if updateList is None or len(updateList) == 0:
            return
        
        #dbTable == data 1:1 match
        self.slot[0] = updateList[0]
        self.slot[1] = updateList[1]
        
        self.syncToResp()
    
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            dataIdx = datas[0]
            if not dataIdx in self.ig_container:
                self.ig_container[dataIdx] = Item()
            
            item = self.ig_container[dataIdx]
            item.loadValueFromDB(datas[0], datas[2], datas[3])
            
            if item.itemIdx in self.slot:
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
    
    def setItem(self, itemIdx, itemId, itemCount):
        if itemIdx in self.ig_container:
            self.ig_container[itemIdx].count += itemCount
            self.ig_container[itemIdx].syncToResp()
            return None
        
        self.ig_container[itemIdx] = Item()
        self.ig_container[itemIdx].loadValueFromDB(itemIdx, itemId, itemCount)
        self.ig_container[itemIdx].syncToResp()
                
    def equipItem(self, slotId, itemIdx):
        if slotId > len(self.slot) - 1:
            return Define.ERROR_OUT_OF_RANGE
        
        equipItem = self.getItemByIdx(itemIdx)
        if equipItem is None:
            return Define.ERROR_NOT_FOUND_ITEM
        
        prevIdx = self.slot[slotId]
        
        if prevIdx == itemIdx or equipItem.equip == 1:
            return Define.ERROR_ALREADY_EQUIP_ITEM
        
        self.slot[slotId] = itemIdx        
        
        if not prevIdx is 0:
            prevItem = self.getItemByIdx(prevIdx)
            prevItem.equip = 0
        
        equipItem.equip = 1
        return Define.OK_EQUIP_ITEM
    
    def unEquipItem(self, slotId, itemIdx):
        if slotId > len(self.slot) - 1:
            return Define.ERROR_OUT_OF_RANGE
        
        unEquipItemIdx = self.slot[slotId]
        if unEquipItemIdx == 0 or unEquipItemIdx != itemIdx:
            return Define.ERROR_NOT_FOUND_ITEM
        
        equipItem = self.getItemByIdx(itemIdx)
        equipItem.equip = 0
        self.slot[slotId] = 0
        
        return Define.OK_UNEQUIP_ITEM
    
    def getSlotItem(self, slotId):
        return self.slot[slotId]
    
    
    
class MailContainer(Common.BaseContainerResp):
    def loadValueFromDB(self, updateList):
        for datas in updateList:
            datas = list(datas)
            dataIdx = datas[0]
            if not dataIdx in self.ig_container:
                self.ig_container[dataIdx] = Mail()
            
            mail = self.ig_container[dataIdx]
            mail.loadValueFromDB(datas)
            mail.syncToResp()
            
    def getMailById(self, mailIdx):
        for mail in self.ig_container.values():
            if mail.mailIdx == mailIdx:
                return mail
            
        return None
    
    def removeMailById(self, mailIdx):
        if not mailIdx in self.ig_container:
            return
        
        del self.ig_container[mailIdx]
    