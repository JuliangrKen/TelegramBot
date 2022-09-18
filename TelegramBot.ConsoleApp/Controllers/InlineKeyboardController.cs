using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.ConsoleApp.Services;

namespace TelegramBot.ConsoleApp.Controllers
{
    public class InlineKeyboardController
    {
        protected ITelegramBotClient telegramBotClient;
        protected IStorage memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            this.telegramBotClient = telegramBotClient;
            this.memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку");

            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            // Генерим информационное сообщение
            string languageText = callbackQuery.Data switch
            {
                "ru" => "🇷🇺 Русский",
                "en" => "🇬🇧 Английский",
                _ => string.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}