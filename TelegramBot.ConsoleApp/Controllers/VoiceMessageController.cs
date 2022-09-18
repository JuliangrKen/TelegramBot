using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.ConsoleApp.Services;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class VoiceMessageController : AbstractMessageController
    {
        private readonly IFileHandler audioFileHandler;

        public VoiceMessageController(ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler) : base(telegramBotClient)
        {
            this.audioFileHandler = audioFileHandler;
        }

        public override async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null) return;

            await audioFileHandler.Download(fileId, ct);
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);
        }
    }
}