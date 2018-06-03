from Route import Login
from flask_restful import reqparse
from Route.Login import Login
from Route.Shop import ShopBuyProduct
from Route.Mail import MailPostRead
from Route.Mail import MailPostAccept
from Route.Mail import MailWrite
from Route.User import InventoryEquip
from Route.User import InventoryUnEquip

route_dict = { 
    Login:'/loginInfo', 
    ShopBuyProduct:'/shop/buyProduct',
    MailPostRead : '/mailPost/read',
    MailPostAccept : '/mailPost/accept',
    MailWrite : '/mailPost/write',
    InventoryEquip : '/inventory/equip',
    InventoryUnEquip : '/inventory/unequip'
}

parser = reqparse.RequestParser()