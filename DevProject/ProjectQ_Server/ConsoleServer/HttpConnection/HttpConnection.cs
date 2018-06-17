using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Http
{
    public class HttpConnection
    {
        RestClient m_restClient;
        static readonly JsonSerializer _serializer = new JsonSerializer {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            DefaultValueHandling = DefaultValueHandling.Include
        };

        public HttpConnection(string url)
        {
            m_restClient = new RestClient(url);
            m_restClient.CookieContainer = new System.Net.CookieContainer();
        }

        public void HttpConnectAsync<REQUEST, RESPONSE>(ulong session, REQUEST dto, Action<RESPONSE> callback) 
            where REQUEST : new()
            where RESPONSE : new()
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

            m_restClient.ExecuteAsync(request, (response) => {
                var result = JsonConvert.DeserializeObject<RESPONSE>(response.Content);
                callback.Invoke(result);
            });
        }

        public void HttpConnectAsync<REQUEST, RESPONSE>(REQUEST dto, Action<RESPONSE> callback)
    where REQUEST : new()
    where RESPONSE : new()
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

            m_restClient.ExecuteAsync(request, (response) => {
                var result = JsonConvert.DeserializeObject<RESPONSE>(response.Content);
                callback.Invoke(result);
            });
        }

        public void HttpExecuteAsync(RestRequest request, Action<IRestResponse> response)
        {
            m_restClient.ExecuteAsync(request, response);
        }

        public void HttpExecute(RestRequest request)
        {
            m_restClient.Execute(request);
        }
    }
}
