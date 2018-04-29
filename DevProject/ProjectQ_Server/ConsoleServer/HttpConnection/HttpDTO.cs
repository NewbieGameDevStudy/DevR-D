using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    public class ResponseBase
    {
        public int responseCode;
    }

    #region 오류 코드
    //const int ERROR_

    #endregion

    #region 플레이어 관련 요청
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
}
