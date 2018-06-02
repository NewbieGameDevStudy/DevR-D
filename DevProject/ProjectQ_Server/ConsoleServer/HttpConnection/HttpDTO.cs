﻿using Http;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GameServer.Connection
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
        public string portrait;
    }

    [HttpConnect(Method.GET, "/loginInfo")]     //두번째
    public class ReqLoginInfo
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
    }

    public class Item
    {
        public ulong itemId;
        public int itemIdx;        
        public int itemType;
    }

    public class ItemContainer
    {
        public Item[] item;
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
    }

    public class MailContainer
    {
        public Mail[] mail;
    }

    public class PlayerStatus : ResponseBase
    {
        public Account Account;
        public ItemContainer itemContainer;
        public MailContainer mail;
    }
    
    #endregion

    public static class ResponseCode
    {
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_NOT_FOUND_SESSION = 103;
        public const int ERROR_INPUT_PARAMS = 102;
        public const int ERROR_INVALID_ACCESS = 104;


        public const int ERROR_CREATE_NOT_LOGIN = 1001;
        public const int ERROR_ALREADY_CREATE_NICKNAME = 1002;
        public const int OK_CREATE_LOGIN = 2001;
        public const int OK_LOGIN_CONNECT = 2002;

        public const int OK_SUCCESS = 2004;

        public const int ERROR_INVALID_BUY_PRODUCT = 30001;
        public const int ERROR_NOT_ENOUGH_MONEY = 30002;
        public const int ERROR_NOT_FOUND_ITEM = 30003;
        
    }

}
