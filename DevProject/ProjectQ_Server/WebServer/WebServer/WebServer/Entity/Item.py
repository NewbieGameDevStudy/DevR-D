from MetaDataMgr import metaData
from Route import Common

class Item(Common.BaseObjResp):
    def __init__(self):
        super(Item, self).__init__()
        self.itemIdx = 0
        self.itemId = 0        
        self.ig_metaDict = {}
        self.itemType = "" 
        self.equip = 0
        self.count = 0
        self.initRespCache()
    
    def loadValueFromDB(self, itemIdx, itemId, count):
        self.itemIdx = itemIdx
        self.count = count
        if self.itemId != itemId:
            self.itemId = itemId        
            self.ig_metaDict = metaData.getMetaData("ShopItem", self.itemId)
            self.itemType = self.ig_metaDict['ItemType']            
             
    def getResp(self):
        return self.ig_respDict
    
