using Sep.App.Telegram.Helpers;
using Sep.App.Telegram.Telegram;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Sep.App.Telegram
{
    [SeqApp("Telegram App",
        Description = "Telegram package for Seq")]
    public class SeqAppTelegramReactor : Reactor, ISubscribeTo<LogEventData>
    {
        [SeqAppSetting(DisplayName = "Bot Token",
           HelpText = "Telegram Bot Token",
           InputType = SettingInputType.Text)]
        public string BotToken { get; set; }

        [SeqAppSetting(DisplayName = "Chat Id",
            HelpText = "Chat ID that the notification is sent to",
            InputType = SettingInputType.Integer)]
        public string ChatId { get; set; }

        [SeqAppSetting(DisplayName = "Message Template\n" +
            " Replaces:\n" +
            "		{Level}\n" +
            "		{Message}\n" +
            "		{DateTime}\n" +
            "       {Property.Count}\n" +
            "		{Property.Condition}\n" +
            "		{Property.ChartTitle}\n" +
            "		{Property.AlertRangeEnd}\n" +
            "		{Property.DashboardTitle}\n" +
            "		{Property.AlertRangeStart}\n" ,          
            HelpText = "*For example: " +
            "\"Message - {Message} Level - {Level} Count - {Property.Count}\"",
            InputType = SettingInputType.LongText, 
            IsOptional = true)]

        public string MessageTemplate { get; set; }

        public void On(Event<LogEventData> evt)
        {
            var message = MessageHellper.GenerateMessage(evt, MessageTemplate);

            if(message.Length > 0)
                TelegramBot.SendMessage(BotToken, ChatId, message);
        }
    }
}
