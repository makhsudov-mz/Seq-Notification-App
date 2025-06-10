using System.Net;

namespace Sep.App.Telegram.Telegram
{
    public static class TelegramBot
    {
        public static void SendMessage(string token, string chatId, string message)
        {
            string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={message}";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var webClient = new WebClient())
            {
                webClient.DownloadString(url);
            }
        }
    }
}
