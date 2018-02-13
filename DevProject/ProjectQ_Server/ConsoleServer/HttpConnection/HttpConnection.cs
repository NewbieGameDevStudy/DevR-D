using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

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
        }

        public void HttpConnectAsync<T>(T dto, Action<T> callback) where T : new()
        {
            var type = dto.GetType();
            var attribute = type.GetCustomAttributes(typeof(HttpConnectAttribute), true).FirstOrDefault() as HttpConnectAttribute;

            var request = new RestRequest {
                Method = attribute.RequestMethod,
                Resource = attribute.Resource,
                RequestFormat = DataFormat.Json
            };
            request.AddBody(dto);

            m_restClient.ExecuteAsync(request, (response) => {
                var result = JsonConvert.DeserializeObject<T>(response.Content);
                callback(result);
            });
        }
    }
}
