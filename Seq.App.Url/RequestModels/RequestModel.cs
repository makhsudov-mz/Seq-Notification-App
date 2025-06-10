using Newtonsoft.Json;
using System;

namespace Seq.App.Url.RequestModels
{
    public class RequestModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("dashboard_title")]
        public string DashboardTitle { get; set; }

        [JsonProperty("chart_title")]
        public string ChartTitle { get; set; }

        [JsonProperty("create_at")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
