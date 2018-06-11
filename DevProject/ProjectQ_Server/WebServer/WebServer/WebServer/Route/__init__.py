from Route import Login
from flask_restful import reqparse
from Route.Login import Login
from Route.Shop import ShopBuyProduct
from Route.Mail import MailPostRead
from Route.Mail import MailPostAccept
from Route.Mail import MailPostDone
from Route.Mail import MailWrite
from Route.Inventory import InventoryEquip
from Route.Inventory import InventoryUnEquip
from Route.User import UserFind
from Route.Guild import GuildCreate
from Route.Guild import GuildJoin

route_dict = { 
    Login:'/loginInfo', 
    ShopBuyProduct:'/shop/buyProduct',
    MailPostRead : '/mailPost/read',
    MailPostDone : '/mailPost/done',
    MailPostAccept : '/mailPost/accept',
    MailWrite : '/mailPost/write',    
    InventoryEquip : '/inventory/equip',
    InventoryUnEquip : '/inventory/unequip',
    UserFind : '/user/find',
    GuildCreate : '/guild/create',
    GuildJoin : '/guild/join',
}

parser = reqparse.RequestParser()