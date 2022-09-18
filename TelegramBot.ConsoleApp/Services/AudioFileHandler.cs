using Telegram.Bot;
using TelegramBot.ConsoleApp.Configuration;
using TelegramBot.ConsoleApp.Utilities;

namespace TelegramBot.ConsoleApp.Services
{
    public class AudioFileHandler : IFileHandler
    {
        private readonly BotConfig botConfig;
        private readonly ITelegramBotClient telegramBotClient;

        public AudioFileHandler(ITelegramBotClient telegramBotClient, BotConfig botConfig)
        {
            // Генерируем полный путь файла из конфигурации
            this.botConfig = botConfig;
            this.telegramBotClient = telegramBotClient;
        }

        public async Task Download(string fileId, CancellationToken ct)
        {
            // Загружаем информацию о файле
            var file = await telegramBotClient.GetFileAsync(fileId, ct);
            if (file.FilePath == null)
                return;

            var fullPathToAudioFile = Path.Combine(botConfig.DownloadsFolder ?? Environment.CurrentDirectory, $"{botConfig.AudioFileName ?? "Audio"}.ogg");

            // Скачиваем файл
            using FileStream destinationStream = File.Create(fullPathToAudioFile);
            await telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, ct);
        }

        public string Process(string languageCode)
        {
            var inputAudioPath = Path.Combine(botConfig.DownloadsFolder ?? Environment.CurrentDirectory, $"{botConfig.AudioFileName ?? "Audio"}.ogg");
            var outputAudioPath = Path.Combine(botConfig.DownloadsFolder ?? Environment.CurrentDirectory, $"{botConfig.AudioFileName ?? "Audio"}.wav");

            Console.WriteLine("Начинаем конвертацию...");
            AudioConverter.TryConvert(inputAudioPath, outputAudioPath);
            Console.WriteLine("Файл конвертирован");

            Console.WriteLine("Начинаем распознавание...");
            var speechText = SpeechDetector.DetectSpeech(outputAudioPath, 48000, languageCode);
            Console.WriteLine("Файл распознан.");
            return speechText;
        }
    }
}