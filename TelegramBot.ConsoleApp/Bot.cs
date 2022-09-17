using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace TelegramBot.ConsoleApp
{
    internal class Bot
    {
        private readonly ITelegramBotClient _telegramClient;

        public Bot(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message == null)
                return;

            //  Обрабатываем нажатия на кнопки из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку", cancellationToken: cancellationToken);
                return;
            }

            // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.Message)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы отправили сообщение", cancellationToken: cancellationToken);
                return;
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => 
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);

            // Задержка перед повторным подключением
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}