using Seq.Apps;
using Seq.Apps.LogEvents;
using Seq.App.Url.Modules.HttpClient;
using Seq.App.Url.Helpers;

namespace Seq.App.Url
{
    [SeqApp("Seq App Url",
         Description = "Seq app for EO. Sends alerts by URL ")]
    public class SeqAppUrlReactor : Reactor, ISubscribeTo<LogEventData>
    {
        [SeqAppSetting(DisplayName = "BaseUrl",
            HelpText = "Add the URL to send the data." +
            "For example: https//example.tj/api/v1")]
        public string BaseUrl { get; set; }

        public void On(Event<LogEventData> evt)
        {
            var request = Helper.GetRequestModel(evt);

            HttpClient httpClient = new HttpClient(BaseUrl);
            httpClient.PostRequest(request);
        }
    }
}