using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class TextMessageController : AbstractMessageController
    {
        public TextMessageController(ITelegramBotClient telegramBotClient) : base(telegramBotClient)
        {
        }

        public override async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData($"🇷🇺 Русский" , $"ru"),
                            InlineKeyboardButton.WithCallbackData($"🇬🇧 English" , $"en")
                        }
                    };

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот превращает аудио в текст.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно записать сообщение и переслать другу, если лень печатать.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                    break;
            }
        }
    }
}