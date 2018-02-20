using RestSharp;
using System;

namespace Http
{
    public class HttpConnectAttribute : Attribute
    {
        public Method RequestMethod { get; set; }
        public string Resource { get; set; }

        public HttpConnectAttribute(Method method, string resource)
        {
            RequestMethod = method;
            Resource = resource;
        }
    }
}
