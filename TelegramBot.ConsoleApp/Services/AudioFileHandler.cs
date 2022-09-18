using Telegram.Bot;
using TelegramBot.ConsoleApp.Configuration;

namespace TelegramBot.ConsoleApp.Services
{
    public class AudioFileHandler : IFileHandler
    {
        private readonly string fullPathToFile;
        private readonly ITelegramBotClient telegramBotClient;

        public AudioFileHandler(ITelegramBotClient telegramBotClient, BotConfig botConfig)
        {
            // Генерируем полный путь файла из конфигурации
            fullPathToFile = Path.Combine(botConfig.DownloadsFolder ?? Environment.CurrentDirectory, $"{botConfig.AudioFileName ?? "Audio"}.ogg");
            this.telegramBotClient = telegramBotClient;
        }

        public async Task Download(string fileId, CancellationToken ct)
        {
            // Загружаем информацию о файле
            var file = await telegramBotClient.GetFileAsync(fileId, ct);
            if (file.FilePath == null)
                return;
            
            // Скачиваем файл
            using FileStream destinationStream = File.Create(fullPathToFile);
            await telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, ct);
        }

        public string Process(string languageCode)
        {
            // Метод пока не реализован
            throw new NotImplementedException();
        }
    }
}
