using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class TextMessageController : AbstractMessageController
    {
        public TextMessageController(ITelegramBotClient telegramBotClient) : base(telegramBotClient)
        {
        }

        public override async Task Hangle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено текстовое сообщение", cancellationToken: ct);
        }
    }
}