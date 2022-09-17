using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.ConsoleApp.Controllers
{
    /// <summary>
    /// Абстрактный класс для создания контроллеров
    /// </summary>
    public abstract class AbstractController
    {
        /// <summary>
        /// Поле, хранящее ссылку на клиент бота телеграм
        /// </summary>
        protected readonly ITelegramBotClient telegramBotClient;

        /// <summary>
        /// Конструктор, инициализирующий telegramBotClient
        /// </summary>
        /// <param name="telegramBotClient"></param>
        public AbstractController(ITelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Обязательный обработчик
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public abstract Task Hangle(Message message, CancellationToken ct);
    }
}