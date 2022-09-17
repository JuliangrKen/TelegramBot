using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Telegram.Bot;
using TelegramBot.ConsoleApp;
using TelegramBot.ConsoleApp.Controllers;
using TelegramBot.ConsoleApp.Services;

// Объект, отвечающий за постоянный жизненный цикл приложения
var host = new HostBuilder()
    .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
    .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
    .Build(); // Собираем

Console.WriteLine("Сервис запущен");

// Запускаем сервис
await host.RunAsync();

Console.WriteLine("Сервис остановлен");

void ConfigureServices(IServiceCollection services)
{
    #region Adding a controllers

    services.AddTransient<DefaultMessageController>();
    services.AddTransient<InlineKeyboardController>();
    services.AddTransient<TextMessageController>();
    services.AddTransient<VoiceMessageController>();

    #endregion

    // Регистрируем метод получения конфигурации бота
    services.AddSingleton(GetBotConfig());
    var botConfig = GetBotConfig();
    // Регистрируем объект TelegramBotClient c токеном подключения
    services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(botConfig.Token ?? ""));
    // Регистрируем постоянно активный сервис бота
    services.AddHostedService<Bot>();
    // Регистрируем сервис получения данных о сессии пользователя
    services.AddSingleton<IStorage, MemoryStorage>();
}

BotConfig GetBotConfig()
{
    // Достаём из конфига объект и десериализуем его строчкой ниже в модель.
    var json = File.ReadAllText($@"{Environment.CurrentDirectory}/BotConfig.json");
    return JsonSerializer.Deserialize<BotConfig>(json) ?? throw new ArgumentNullException();
}