using Http;
using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BaseClient
{
    public partial class Client
    {
        private HttpConnection HttpConnection { get; set; }

        public Queue<HttpCommand> httpReqQueue = new Queue<HttpCommand>();

        public class HttpCommand
        {
            public enum CommandState
            {
                None = 0,
                Req,
                End,
            }
            public RestRequest req;
            public Action callback;
            public IRestResponse response;
            public CommandState state;
        }

        public void HttpConnect(string ip = "localhost")
        {
            HttpConnection = new HttpConnection(string.Format("http://{0}:5000", ip));
        }

        public void WebReqEnqueue<REQUEST, RESPONSE>(ulong session, REQUEST dto, Action<RESPONSE> callback)
        {
            var type = dto.GetType();
            var attribute = type.GetCustomAttributes(typeof(HttpConnectAttribute), true).FirstOrDefault() as HttpConnectAttribute;

            var request = new RestRequest {
                Method = attribute.RequestMethod,
                Resource = attribute.Resource,
                RequestFormat = DataFormat.Json
            };

            if (attribute.RequestMethod == Method.GET) {
                var fields = type.GetFields();
                foreach (var info in fields) {
                    request.AddParameter(info.Name, info.GetValue(dto));
                }
            } else {
                request.AddBody(dto);
            }

            request.AddCookie("Session", session.ToString());

            var command = new HttpCommand();
            command.req = request;
            command.callback = () => {
                var result = JsonConvert.DeserializeObject<RESPONSE>(command.response.Content);
                callback.Invoke(result);
            };
            command.state = HttpCommand.CommandState.None;

            httpReqQueue.Enqueue(command);
        }

        public void HttpReqUpdate()
        {
            if (httpReqQueue.Count == 0)
                return;

            var reqData = httpReqQueue.Peek();

            switch (reqData.state) {
                case HttpCommand.CommandState.None:
                    HttpConnection.HttpExecuteAsync(reqData.req, (response) => {
                        reqData.response = response;
                        reqData.state = HttpCommand.CommandState.End;
                    });
                    reqData.state = HttpCommand.CommandState.Req;
                    break;
                case HttpCommand.CommandState.End:
                    reqData.callback();
                    httpReqQueue.Dequeue();
                    break;
            }
        }
    }
}
