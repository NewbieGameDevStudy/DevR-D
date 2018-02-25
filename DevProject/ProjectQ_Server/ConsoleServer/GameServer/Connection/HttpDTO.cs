using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    #region 플레이어 관련 요청
    [HttpConnect(Method.GET, "/playerinfo")]
    public class ReqPlayerInfo
    {
        public int accountId;
        public int accountId2;
    }

    public class RespPlayerInfo
    {
        public int level;
        public int exp;
    }
    #endregion
}
