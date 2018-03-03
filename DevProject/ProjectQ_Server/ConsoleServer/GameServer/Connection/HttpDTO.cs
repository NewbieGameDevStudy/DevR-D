using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    #region 플레이어 관련 요청
    [HttpConnect(Method.GET, "/playerinfo")]
    public class ReqPlayerInfo
    {
        public ulong accountId;
    }

    public class RespPlayerInfo
    {
        public bool result;
        public int level;
        public int exp;
        public int gameMoney;
        public string name;
    }
    #endregion
}
