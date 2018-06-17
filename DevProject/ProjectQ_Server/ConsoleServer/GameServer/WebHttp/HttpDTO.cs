using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.WebHttp
{
    #region 공용 프로토콜 정의
    public class ResponseBase
    {
        public int responseCode;
    }

    [HttpConnect(Method.GET, "/gameserver/userInfo")]
    public class ReqUserInfo
    {
        public ulong accountId;
    }

    [HttpConnect(Method.GET, "/gameserver/userInfos")]
    public class ReqUserInfos
    {
        public ulong[] accountIds;
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

    public class User
    {        
        public string name;
        public int level;
        public int exp;
        public int portrait;
    }

    #endregion

    #region response 응답 클래스 

    public class UserInfo : ResponseBase
    {
        public User userInfo;
    }

    public class UserInfos : ResponseBase
    {
        public User[] userInfos;
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

    }

}
