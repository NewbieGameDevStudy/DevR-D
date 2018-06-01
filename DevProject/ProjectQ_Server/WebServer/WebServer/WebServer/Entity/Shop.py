'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import SHOP, ITEM_CONTANIER
from MetaDataMgr import metaData
from Route import Common
import Route.Common

class ShopBase(object):
    def __init__(self):
        self.itemMetas = metaData.getMetaDatas("ShopItem")
        
    
    def getMetaData(self, itemId):
        return self.itemMetas[itemId]
    
    def buyProduct(self, buyProductId, userObject):
        
        itemContainer = userObject.getData(ITEM_CONTANIER)
        
        if not buyProductId in self.itemMetas:
            return Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_ITEM)
        
        itemInfo = self.itemMetas[buyProductId]
        
        findItem = itemContainer.getItemById(buyProductId)