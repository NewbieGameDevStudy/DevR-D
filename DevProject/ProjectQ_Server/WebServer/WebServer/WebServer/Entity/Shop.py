'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import SHOP
from MetaDataMgr import metaData

class ShopBase(object):
    def __init__(self):
        self.itemMetas = metaData.getMetaDatas("ShopItem")
        
    
    def getMetaData(self, itemId):
        return self.itemMetas[itemId]
    
    def buyProduct(self, buyProductId):
        