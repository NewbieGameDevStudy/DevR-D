from Route import Login
from flask_restful import reqparse
from Route.Login import Login, LogOut
from Route.Shop import ShopBuyProduct
from Route.Mail import MailPostRead, MailPostAccept, MailPostDone, MailWrite, MailPostDelete
from Route.Slot import SlotEquip, SlotUnEquip
from Route.User import UserFind, UserInfo, UserInfos
from Route.Guild import GuildCreate, GuildLeave, GuildKick, GuildJoin, GuildList

route_dict = { 
    Login:'/loginInfo', 
    LogOut:'/logout',
    ShopBuyProduct:'/shop/buyProduct',
    MailPostRead : '/mailPost/read',
    MailPostDone : '/mailPost/done',
    MailPostAccept : '/mailPost/accept',
    MailPostDelete : '/mailPost/delete',
    MailWrite : '/mailPost/write',    
    SlotEquip : '/slot/equip',
    SlotUnEquip : '/slot/unequip',
    UserFind : '/user/find',
    GuildCreate : '/guild/create',
    GuildJoin : '/guild/join',
    GuildLeave : '/guild/leave',
    GuildKick : '/guild/kick',
    GuildList : '/guild/list',
    
    #gameServer Response
    UserInfo : '/gameserver/userInfo',
    UserInfos : '/gameserver/userInfos'
}

parser = reqparse.RequestParser()