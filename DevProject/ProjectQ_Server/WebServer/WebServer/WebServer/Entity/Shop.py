'''
Created on 2018. 5. 13.

@author: namoeye
'''
from Entity.Define import SHOP, ITEM_CONTANIER
from MetaDataMgr import metaData
from Route import Common
from Entity import Define
import Route.Common
import DB

class ShopBase(object):
    def __init__(self):
        self.itemMetas = metaData.getMetaDatas("ShopItem")
        
    def getMetaData(self, itemId):
        return self.itemMetas[itemId]
    
    def buyProduct(self, buyProductId, buyProductCount, userObject):
        if not buyProductId in self.itemMetas:
            return Common.respHandler.customeResponse(Route.Define.ERROR_NOT_FOUND_ITEM)
        
        buyItemInfo = self.itemMetas[buyProductId]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if buyProductCount <= 0:
            return Common.respHandler.customeResponse(Route.Define.ERROR_INVALID_BUY_PRODUCT)
        
        if "Single" == buyItemInfo['ItemType']:
            return Common.respHandler.customeResponse(Route.Define.ERROR_REQUEST_SINGLE_ITEM)
        
        priceValue = buyItemInfo['Price']
        
        if priceValue > accountInfo.gameMoney:
            return Common.respHandler.customeResponse(Route.Define.ERROR_NOT_ENOUGH_MONEY)
        
        itemContainer = userObject.getData(ITEM_CONTANIER)
        
        #1 == no stock item, 0 == stock item
        typeStr = buyItemInfo['ItemType']
        if typeStr == "Normal":
            findItem = itemContainer.getItemById(buyProductId)
            if not findItem is None: 
                return Common.respHandler.customeResponse(Route.Define.ERROR_ALREADY_BUY_NO_STOCK_ITEM)
            
            if buyProductCount > 1:
                return Common.respHandler.customeResponse(Route.Define.ERROR_ONLY_ONE_PURCHASE_AVAILABLE)
            
        itemId = buyItemInfo['Index']
        out_ItemIdx = 0
        
        #outputParams = Range(>=, <)
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Item_BuyProduct", (itemId, accountInfo.accountId, buyProductCount, priceValue, out_ItemIdx), (4, 4))
        except Exception as e:
            print(str(e))
            return Common.respHandler.customeResponse(Route.Define.ERROR_DB)
        
        accountInfo.gameMoney -= priceValue
        accountInfo.syncToResp()
        itemContainer.setItem(resultDB[0], itemId, buyProductCount)
        
        return Common.respHandler.customeResponse(Route.Define.OK_SHOP_BUY_PRODUCT, {"gameMoney" : accountInfo.gameMoney, "buyItemId" : resultDB[0], "buyItemCount":buyProductCount})
        
