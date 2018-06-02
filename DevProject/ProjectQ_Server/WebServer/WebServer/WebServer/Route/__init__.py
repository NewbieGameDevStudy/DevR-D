from Route import Login
from flask_restful import reqparse
from Route.Login import Login
from Route.Shop import TradeShop
from Route.Mail import MailPostRead
from Route.Mail import MailPostAccept
from Route.Mail import MailWrite

route_dict = { 
    Login:'/loginInfo', 
    TradeShop:'/tradeShop',
    MailPostRead : '/mailPost/read',
    MailPostAccept : '/mailPost/accept',
    MailWrite : '/mailPost/write'
}

parser = reqparse.RequestParser()