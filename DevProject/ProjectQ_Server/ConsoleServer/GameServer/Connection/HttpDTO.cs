using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    [HttpConnect(Method.GET, "/test")]
    public class TestDTO
    {
        public int a;
        public List<int> list;
    }
}
