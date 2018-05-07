using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    public class Protocol
    {
        #region 공용 프로토콜 정의
        public class ResponseBase
        {
            public int responseCode;
        }

        #region 응답값
        public const int RESPONSE_OK = 201;
        #endregion

        #endregion


        #region 컨텐츠별 프로토콜

        #region Login 프로토콜
        //ERROR
        public const int ERROR_LOGIN_NOT_FOUND_ACCOUNT = 101;
        public const int ERROR_LOGIN_FAILED_PARAM = 102;
        public const int ERROR_CREATE_NOT_LOGIN = 103;

        //SUCCESS
        public const int SUCCESS_CREATE_LOGIN = 2001;

        [HttpConnect(Method.GET, "/loginInfo")]
        public class ReqPlayerInfo
        {
            public ulong accountId;
        }

        public class RespPlayerInfo : ResponseBase
        {
            public bool result;
            public int level;
            public int exp;
            public int gameMoney;
            public string name;
        }
        #endregion

        #endregion
    }
}
