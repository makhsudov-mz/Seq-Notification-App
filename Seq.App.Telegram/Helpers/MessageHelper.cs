using Newtonsoft.Json;

using Seq.Apps;
using Seq.Apps.LogEvents;

using System;
using System.Collections.Generic;

namespace Sep.App.Telegram.Helpers
{
    public static class MessageHellper
    {
        enum Lengths
        {
            ValueLength = 8
        }

        private static string DefaultTemplate = "Message - {Message}\n" +
                                                "Level - {Level}\n" +
                                                "Count - {Property.Count}\n" +
                                                "Dashboard Title - {DashboardTitle}\n" +
                                                "Chart Title - {ChartTitle}\n" +
                                                "Date Time - {DateTime}";


        public static string GenerateMessage(Event<LogEventData> evt, string messageTemplate)
        {
            var message = messageTemplate == null ? DefaultTemplate : messageTemplate;

            message = message.Replace("{Message}", evt?.Data?.RenderedMessage);
            message = message.Replace("{Level}", evt?.Data?.Level.ToString());
            message = message.Replace("{DateTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:fff"));
            message = message.Replace("{LocalTimeStamp}", evt?.Data.LocalTimestamp.ToString());

            foreach (var property in evt?.Data?.Properties)
            {
                switch (property.Key)
                {
                    case "DashboardTitle":
                        message = message.Replace("{Property.DashboardTitle}", property.Value.ToString());
                        break;
                    case "DashboardUrl":
                        message = message.Replace("{Property.DashboardUrl}", property.Value.ToString());
                        break;
                    case "ChartTitle":
                        message = message.Replace("{Property.ChartTitle}", property.Value.ToString());
                        break;
                    case "AlertRangeStart":
                        message = message.Replace("{Property.AlertRangeStart}", GetDateTime(property.Value.ToString()));
                        break;
                    case "AlertRangeEnd":
                        message = message.Replace("{Property.AlertRangeEnd}", GetDateTime(property.Value.ToString()));
                        break;
                    case "Condition":
                        message = message.Replace("{Property.Condition}", GetConditionCount(property.Value.ToString()));
                        break;
                    case "Results":
                        var countStr = GetCountRow(JsonConvert.SerializeObject(property.Value));

                        if (countStr.Length > 0)
                            message = message.Replace("{Property.Count}", countStr);
                        else if (message.IndexOf("{Property.Count}") > -1)
                        {
                            message = "";
                        }
                        break;
                    default:
                        break;
                }
            }

            return message;
        }

        private static string GetDateTime(string TimeZone)
        {
            return DateTime.Parse(TimeZone).ToString("yyyy-MM-dd HH:mm:fff");
        }

        private static string GetConditionCount(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > ((int)Lengths.ValueLength))
            {
                int difference = value.Length - (int)Lengths.ValueLength;
                value = value.Substring(value.Length - difference);
            }

            return value;
        }

        private static string GetCountRow(string value)
        {
            try
            {
                var results = JsonConvert.DeserializeObject<List<Result>>(value);

                foreach (var slices in results[0].Slices)
                {
                    if (slices.Rowset.Count > 0)
                    {
                        var row = slices.Rowset[0];
                        return row[0].ToString();
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
    public class Result
    {
        public List<object> Key { get; set; }

        public List<Slice> Slices { get; set; }
    }

    public class Slice
    {
        public string SliceStart { get; set; }

        public List<List<long>> Rowset { get; set; }
    }
}
