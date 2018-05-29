from MetaDataMgr import metaData
from Route import Common

class Item(Common.ObjRespBase):
    def __init__(self, itemIdx, itemId):
        self.itemIdx = itemIdx
        self.itemId = itemId        
        self.metaDict = metaData.getMetaData("ShopItem", self.itemId)
        self.itemType = self.metaDict['ItemType'] 
    
    def updateValue(self):
        pass