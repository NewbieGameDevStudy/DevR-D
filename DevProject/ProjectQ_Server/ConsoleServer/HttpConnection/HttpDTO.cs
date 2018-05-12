using Http;
using RestSharp;
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
        public ulong accountId;
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

    public class RespLoginInfo : ResponseBase
    {
        public ulong accountId;
        public int portrait;
        public int bestRecord;
        public int winRecord;
        public int continueRecord;
        public int level;
        public int exp;
        public int gameMoney;
        public string name;
    }
    #endregion

    public static class ResponseCode
    {
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_CREATE_LOGIN_PARAM = 102;


        public const int ERROR_CREATE_NOT_LOGIN = 1001;
        public const int ERROR_ALREADY_CREATE_NICKNAME = 1002;

        public const int OK_CREATE_LOGIN = 2001;
        public const int OK_LOGIN_CONNECT = 2002;
    }

}
