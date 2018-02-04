using RestSharp;
using System;
using System.Collections.Generic;

namespace GameServer.Connection
{
    class HttpConnectAttribute : Attribute
    {
        public Method RequestMethod { get; set; }
        public string Resource { get; set; }

        public HttpConnectAttribute(Method method, string resource)
        {
            RequestMethod = method;
            Resource = resource;
        }
    }

    [HttpConnect(Method.GET, "/test")]
    public class TestDTO
    {
        public int a;
        public List<int> list;
    }
}
