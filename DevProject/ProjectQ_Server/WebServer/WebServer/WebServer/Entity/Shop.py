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
            return Common.respHandler.errorResponse(Route.Define.ERROR_NOT_FOUND_ITEM)
        
        buyItemInfo = self.itemMetas[buyProductId]
        accountInfo = userObject.getData(Define.ACCOUNT_INFO)
        
        if "Single" == buyItemInfo['ItemType']:
            return Route.Define.ERROR_REQUEST_SINGLE_ITEM
        
        priceValue = buyItemInfo['Price']
        
        if priceValue > accountInfo.gameMoney:
            return Route.Define.ERROR_NOT_ENOUGH_MONEY
        
        itemContainer = userObject.getData(ITEM_CONTANIER)
        
        #1 == no stock item, 0 == stock item
        typeStr = buyItemInfo['ItemType']
        if typeStr == "Normal":
            findItem = itemContainer.getItemById(buyProductId)
            if not findItem is None: 
                return Route.Define.ERROR_ALREADY_BUY_NO_STOCK_ITEM
            
        itemId = buyItemInfo['Index']
        out_ItemIdx = 0
        
        #outputParams = Range(>=, <)
        try:
            resultDB = DB.dbConnection.executeStoredProcedure("Game_Item_BuyProduct", (itemId, accountInfo.accountId, buyProductCount, priceValue, out_ItemIdx), (4, 4))
        except Exception as e:
            print(str(e))
            return Route.Define.ERROR_DB
        
        accountInfo.gameMoney -= priceValue
        accountInfo.syncToResp()
        itemContainer.setItem(resultDB[0], itemId, buyProductCount)
        
        return Route.Define.OK_SHOP_BUY_PRODUCT
        
