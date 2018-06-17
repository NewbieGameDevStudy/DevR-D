using Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.WebHttp
{
    public class HttpWebReq
    {
        public HttpConnection HttpConnection { get; private set; }
        public Queue<HttpCommand> ReqQueue = new Queue<HttpCommand>();

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

        public void Connect(string httpUrl, int port)
        {
            HttpConnection = new HttpConnection($"{httpUrl}:{port.ToString()}");
        }

        public void WebReqEnqueue<REQUEST, RESPONSE>(REQUEST dto, Action<RESPONSE> callback)
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
                    var field = info.GetValue(dto);
                    AddParameter(request, info.Name, field);
                }
            } else {
                request.AddBody(dto);
            }

            var command = new HttpCommand();
            command.req = request;
            command.callback = () => {
                var result = JsonConvert.DeserializeObject<RESPONSE>(command.response.Content);
                if (result == null) {
                    Console.WriteLine($"WebReq Error : {command.req.Resource.ToString()}");
                    return;
                }
                callback.Invoke(result);
            };
            command.state = HttpCommand.CommandState.None;

            ReqQueue.Enqueue(command);
        }

        void AddParameter(RestRequest request, string fieldName, object param)
        {
            if (typeof(Array).IsAssignableFrom(param.GetType())) {
                var json = JsonConvert.SerializeObject(param);
                request.AddParameter(fieldName, json);
                return;
            } 

            request.AddParameter(fieldName, param);
        }

        public void UpdateWebReq()
        {
            if (ReqQueue.Count == 0)
                return;

            var reqData = ReqQueue.Peek();

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
                    ReqQueue.Dequeue();
                    break;
            }
        }
    }
}
