using Http;
using System;

namespace BaseClient
{
    public partial class Client
    {
        public HttpConnection HttpConnection { get; private set; }

        public void HttpConnect()
        {
            HttpConnection = new HttpConnection("http://localhost:5000");
        }
    }
}
