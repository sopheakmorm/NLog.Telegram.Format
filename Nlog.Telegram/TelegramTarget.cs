using System;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.Telegram
{
    [Target("Telegram")]
    public class TelegramTarget : TargetWithLayout
    {
        public TelegramTarget()
        {
            this.BaseUrl = "https://api.telegram.org/bot";
            this.OptimizeBufferReuse = true;
        }

        [RequiredParameter]
        public Layout BaseUrl { get; set; }

        [RequiredParameter]
        public Layout BotToken { get; set; }

        [RequiredParameter]
        public Layout ChatId { get; set; }

        protected override void Write(AsyncLogEventInfo info)
        {
            try
            {
                this.Send(info);
            }
            catch (Exception e)
            {
                info.Continuation(e);
            }
        }

        private void Send(AsyncLogEventInfo info)
        {
            var message = RenderLogEvent(Layout, info.LogEvent);
            var baseurl = RenderLogEvent(BaseUrl, info.LogEvent);
            var botToken = RenderLogEvent(BotToken, info.LogEvent);
            var chatId = RenderLogEvent(ChatId, info.LogEvent);

            var uriBuilder = new UriBuilder(baseurl + botToken);
            uriBuilder.Path += "/sendMessage";
            var url = uriBuilder.Uri.ToString();
            var builder = TelegramMessageBuilder
                    .Build(url, message)
                    .ToChat(chatId)
                    .OnError(e => info.Continuation(e))
                ;

            builder.Send();
        }
    }
}