using Newtonsoft.Json;
using Seq.App.Url.RequestModels;
using System.Net;

namespace Seq.App.Url.Modules.HttpClient
{
    public class HttpClient
    {
        private WebClient _client { get; set; }
        private string _baseUrl { get; set; }

        public HttpClient(string baseUrl)
        {
            //для защищенной соединение
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _client = new WebClient();
            _client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";

            _baseUrl = baseUrl;
        }

        public void PostRequest(RequestModel request)
        {
            string stringJsonData = JsonConvert.SerializeObject(request);

            _client.UploadString(_baseUrl, stringJsonData);
        }
    }
}
