using Seq.Apps;
using Seq.Apps.LogEvents;
using Seq.App.Url.RequestModels;

namespace Seq.App.Url.Helpers
{
    public static class Helper
    {
        public static RequestModel GetRequestModel(Event<LogEventData> evt)
        {
            var request = new RequestModel
            {
                Level = evt?.Data?.Level.ToString(),
                Message = evt?.Data.RenderedMessage,
            };

            foreach (var item in evt?.Data?.Properties)
            {
                switch (item.Key)
                {
                    case "DashboardTitle":
                        request.DashboardTitle = item.Value.ToString();
                        break;
                    case "ChartTitle":
                        request.ChartTitle = item.Value.ToString();
                        break;
                    default:
                        break;
                }
            }

            return request;
        }
    }
}
