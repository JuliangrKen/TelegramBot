using Telegram.Bot;

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
        /// <returns></returns>
        public abstract Task Hangle();
    }
}