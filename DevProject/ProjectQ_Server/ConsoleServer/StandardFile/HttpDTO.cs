﻿using Http;
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

    [HttpConnect(Method.POST, "/logout")]     //두번째
    public class ReqLogout
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

    [HttpConnect(Method.PUT, "/mailPost/write")]
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
        public ulong mailIdx;
    }

    [HttpConnect(Method.POST, "/mailPost/delete")]
    public class ReqMailDelete
    {
        public ulong mailIdx;
    }

    [HttpConnect(Method.GET, "/user/find")]
    public class ReqUserFind
    {
        public string nickName;
    }

    [HttpConnect(Method.PUT, "/guild/create")]
    public class ReqGuildCreate
    {
        public string guildName;
        public int guildJoinType;
        public int guildMark;
    }

    [HttpConnect(Method.POST, "/guild/join")]
    public class ReqGuildJoin
    {
        public string guildName;
        public ulong guildIdx;
    }

    [HttpConnect(Method.POST, "/guild/leave")]
    public class ReqGuildLeave
    {
        
    }

    [HttpConnect(Method.POST, "/guild/kick")]
    public class ReqGuildKick
    {
        public ulong kickUserId;
    }

    [HttpConnect(Method.GET, "/guild/list")]
    public class ReqGuildList
    {
        
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
        public ulong itemIdx;
        public int itemId;
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
		public int mailType;
		public int senderPortrait;
        public int senderLv;

    }

    public class MailContainer
    {
        public Mail[] mail;
    }

    public class GuildMemberInfo
    {
        public ulong accountId;
        public string name;
        public int level;
        public int exp;
        public int portrait;
        public int bestRecord;
        public int winRecord;
        public int continueRecord;
    }

    public class GuildContainer
    {
        public ulong idx;
        public string name;
        public int memberCount;
        public int joinType;
        public int leaderId;
        public int leaderId2;
        public int grade;
        public int mark;
        public int score;
        public ulong createTime;

        public GuildMemberInfo[] guildMemberInfo;
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
        public ulong buyItemIdx;
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
        public ulong readMailIdx;
    }

    public class DeleteMail : ResponseBase
    {
        public ulong deleteMailIdx;
    }

    public class FindUser : ResponseBase
    {
        public string nickName;
    }

    public class GuidlList : ResponseBase
    {
        public GuildContainer guildContainer;
    }

    public class GuildCreate : ResponseBase
    {
        public GuildContainer guildContainer;
    }

    public class GuildJoin : ResponseBase
    {
        public GuildContainer guildContainer;
    }

    public class GuildLeave : ResponseBase
    {
        
    }

    public class Guildkick : ResponseBase
    {

    }

    #endregion

    #endregion

    public static class ResponseCode
    {
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_NOT_FOUND_SESSION = 103;
        public const int ERROR_INPUT_PARAMS = 102;
        public const int ERROR_INVALID_ACCESS = 104;
        public const int ERROR_OUT_OF_RANGE = 105;
        public const int ERROR_DB = 106;
        public const int ERROR_NOT_FOUND = 107;
        public const int ERROR_EXCUTE_FAIL = 108;

        //Login Create
        public const int ERROR_CREATE_NOT_LOGIN = 1001;
        public const int ERROR_ALREADY_CREATE_NICKNAME = 1002;
        public const int OK_CREATE_LOGIN = 2001;

        //Success
        public const int OK_LOGIN_CONNECT = 2002;
        public const int OK_SUCCESS = 2003;

        //Shop
        public const int ERROR_INVALID_BUY_PRODUCT = 3001;
        public const int ERROR_NOT_ENOUGH_MONEY = 3002;
        public const int ERROR_NOT_FOUND_ITEM = 3003;
        public const int OK_SHOP_BUY_PRODUCT = 3004;
        public const int ERROR_ALREADY_BUY_NO_STOCK_ITEM = 3005;
        public const int ERROR_REQUEST_SINGLE_ITEM = 3006;
		public const int ERROR_ONLY_ONE_PURCHASE_AVAILABLE = 3007;

        //Inventory
        public const int ERROR_ALREADY_EQUIP_ITEM = 4001;
        public const int OK_EQUIP_ITEM = 4002;
        public const int OK_UNEQUIP_ITEM = 4003;

        //User
        public const int ERROR_NOT_FOUND_USER = 5001;

        //Mail
        public const int ERROR_NOT_WRITE = 6001;
        public const int ERROR_ALREADY_READ_DONE = 6002;


        //Guild
        public const int ERROR_CURRENT_JOIN_GUILD = 7001;
        public const int ERROR_LOW_LEVEL = 7002;
        public const int ERROR_ALREADY_GUILDNAME = 7003;
        public const int ERROR_NOT_CREATE_GUILD = 7004;
        public const int ERROR_NOT_FOUND_JOIN_GUILD = 7005;
        public const int ERROR_FULL_JOIN_GUILDMEMBER = 7006;
        public const int ERROR_JOIN_GUILD = 7007;

        public const int OK_JOIN_SIGN_UP = 7008;
        public const int OK_NOT_FOUND_JOIN_GUILD = 7009;
    }
}
