using Http;
using RestSharp;
using System;
using System.Collections.Generic;

namespace HttpDTO
{
    #region 공용 프로토콜 정의
    public class ResponseBase
    {
        public int responseCode;
    }

    [HttpConnect(Method.PUT, "/loginInfo")]     //처음
    public class ReqFirstLoginInfo
    {
        public string nickname;
        public int portrait;
    }

    [HttpConnect(Method.GET, "/loginInfo")]     //두번째
    public class ReqLoginInfo
    {

    }

    [HttpConnect(Method.POST, "/inventory/equip")]
    public class ReqInventoryEquipItem
    {
        public int slotId;
        public ulong itemIdx;
    }

    [HttpConnect(Method.POST, "/inventory/unequip")]
    public class ReqInventoryUnEquipItem
    {
        public int slotId;
        public ulong itemIdx;
    }

    [HttpConnect(Method.POST, "/shop/buyProduct")]
    public class ReqShopBuyProduct
    {
        public int buyProductId;
        public int buyProductCount;
    }

    [HttpConnect(Method.PUT, "/mailPost/wrtie")]
    public class ReqMailPostWrite
    {
        public string targetNickName;
        public string title;
        public string body;
    }

    [HttpConnect(Method.GET, "/mailPost/read")]
    public class ReqMailGetList
    {

    }

    [HttpConnect(Method.POST, "/mailPost/done")]
    public class ReqMailDone
    {
        public ulong mailId;
    }

    [HttpConnect(Method.GET, "/user/find")]
    public class ReqUserFind
    {
        public string nickName;
    }

    //public class A {
    //    public int a;
    //    public List<int> b;
    //}

    //public class RespLoginInfo : ResponseBase
    //{
    //    public ulong accountId;
    //    public int avatarType;
    //    public int bestRecord;
    //    public int winRecord;
    //    public Dictionary<string, int> testDict;
    //    public List<int> testList;
    //    public int continueRecord;
    //    public bool result;
    //    public int level;
    //    public int exp;
    //    public int gameMoney;
    //    public string name;
    //    public A[] a;
    //    public A b;
    //}

    #region 응답에 포함되는 클래스

    public class CreateId : ResponseBase
    {
        public ulong accountId;
    }

    public class Account
    {
        public ulong accountId;
        public string name;
        public int level;
        public int exp;
        public int gameMoney;
        public int portrait;
        public int bestRecord;
        public int winRecord;
        public int continueRecord;
        public int dailyMailCount;
    }

    public class Item
    {
        public ulong itemId;
        public int itemIdx;
        public int count;
        public int equip;
    }

    public class ItemContainer
    {
        public Item[] item;
        public List<ulong> slot;
    }

    public class Mail
    {
        public ulong mailIdx;
        public ulong senderAccountId;
        public string sender;
        public string title;
        public string body;
        public ulong sendTime;
        public ulong exprireTime;
        public int readDone;
    }

    public class MailContainer
    {
        public Mail[] mail;
    }

    #endregion

    #region response 응답 클래스 

    public class PlayerStatus : ResponseBase
    {
        public Account Account;
        public ItemContainer itemContainer;
        public MailContainer mailContainer;
    }

    public class ItemEquip : ResponseBase
    {
        public ulong equipItemIdx;
        public int slotId;
    }

    public class UnItemEquip : ResponseBase
    {
        public ulong equipItemIdx;
        public int slotId;
    }

    public class ShopBuyProduct : ResponseBase
    {
        public int gameMoney;
        public int buyItemId;
        public int buyCount;
    }

    public class GetMailList : ResponseBase
    {
        public MailContainer mailContainer;
    }

    public class WriteMail : ResponseBase
    {
        public int mailCount;       //메일쓰기 이후 증가된 count
        public int gameMoney;       //메일쓰기 이후 감소된 현재 재화량
    }

    public class DoneMail : ResponseBase
    {
        public ulong readMailId;
    }

    public class FindUser : ResponseBase
    {
        public string nickName;
    }



    #endregion

    #endregion

    public static class ResponseCode
    {
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_INPUT_PARAMS = 102;
        public const int ERROR_NOT_FOUND_SESSION = 103;
        public const int ERROR_INVALID_ACCESS = 104;
        public const int ERROR_OUT_OF_RANGE = 105;
        public const int ERROR_DB = 106;

        public const int ERROR_CREATE_NOT_LOGIN = 1001;
        public const int ERROR_ALREADY_CREATE_NICKNAME = 1002;

        public const int OK_CREATE_LOGIN = 2001;
        public const int OK_LOGIN_CONNECT = 2002;
        public const int OK_SUCCESS = 2003;

        public const int ERROR_INVALID_BUY_PRODUCT = 30001;
        public const int ERROR_NOT_ENOUGH_MONEY = 30002;
        public const int ERROR_NOT_FOUND_ITEM = 30003;
        public const int OK_SHOP_BUY_PRODUCT = 3004;
        public const int ERROR_ALREADY_BUY_NO_STOCK_ITEM = 3005;

        public const int ERROR_ALREADY_EQUIP_ITEM = 40001;
        public const int OK_EQUIP_ITEM = 40002;
        public const int OK_UNEQUIP_ITEM = 40003;

        public const int ERROR_NOT_FOUND_USER = 5001;

        public const int ERROR_NOT_WRITE = 6001;
        public const int ERROR_ALREADY_READ_DONE = 6002;
    }


}
