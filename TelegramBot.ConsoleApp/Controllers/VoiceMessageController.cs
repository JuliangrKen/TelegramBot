using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class VoiceMessageController : AbstractMessageController
    {
        public VoiceMessageController(ITelegramBotClient telegramBotClient) : base(telegramBotClient)
        {
        }

        public override async Task Hangle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено голосовое сообщение", cancellationToken: ct);
        }
    }
}