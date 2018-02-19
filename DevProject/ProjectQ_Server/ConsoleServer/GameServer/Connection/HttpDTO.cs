using Http;
using RestSharp;
using System.Collections.Generic;

namespace GameServer.Connection
{
    [HttpConnect(Method.GET, "http://localhost:5000/")]
    public class TestDTO
    {
        public int a;
        public List<int> list;
        public string strTemp;
    }

    [HttpConnect(Method.POST, "http://localhost:5000/test")]
    public class TestDTO_Post
    {
        public int userId;
        public string strNick;
    }
}
