from MetaDataMgr import metaData
from Route import Common

class Item(Common.BaseObjResp):
    def __init__(self):
        super(Item, self).__init__()
        self.itemIdx = 0
        self.itemId = 0        
        self.ig_metaDict = {}
        self.itemType = 0 
        self.initRespCache()
    
    def loadValueFromDB(self, itemIdx, itemId):
        self.itemIdx = itemIdx
        
        if self.itemId != itemId:
            self.itemId = itemId        
            self.ig_metaDict = metaData.getMetaData("ShopItem", self.itemId)
            self.itemType = self.ig_metaDict['ItemType']            
             
    def getResp(self):
        return self.ig_respDict