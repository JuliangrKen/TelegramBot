using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.ConsoleApp.Services;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class VoiceMessageController : AbstractMessageController
    {
        private readonly IFileHandler audioFileHandler;
        private readonly IStorage memoryStorage;

        public VoiceMessageController(ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler, IStorage memoryStorage) : base(telegramBotClient)
        {
            this.audioFileHandler = audioFileHandler;
            this.memoryStorage = memoryStorage;
        }

        public override async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null) return;

            await audioFileHandler.Download(fileId, ct);
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);

            audioFileHandler.Process(memoryStorage.GetSession(message.Chat.Id).LanguageCode ?? "ru");
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение конвертировано в формат .WAV", cancellationToken: ct);
        }
    }
}