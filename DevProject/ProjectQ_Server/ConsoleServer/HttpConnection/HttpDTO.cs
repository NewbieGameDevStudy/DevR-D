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

    [HttpConnect(Method.GET, "/loginInfo")]
    public class ReqPlayerInfo
    {
        public ulong accountId;
    }

    public class A {
        public int a;
        public List<int> b;
    }
    public class RespPlayerInfo : ResponseBase
    {
        public int accountId;
        public int avatarType;
        public int bestRecord;
        public int winRecord;
        public Dictionary<string, int> testDict;
        public List<int> testList;
        public int continueRecord;
        public bool result;
        public int level;
        public int exp;
        public int gameMoney;
        public string name;
        public A[] a;
        public A b;
    }
    #endregion

    public static class ResponseCode
    {
        #region 응답값
        public const int RESPONSE_OK = 201;
        #endregion

        #region Login 응답값
        //ERROR
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_CREATE_LOGIN_PARAM = 102;

        //SUCCESS
        public const int SUCCESS_CREATE_LOGIN = 2001;

        #endregion
    }
}
