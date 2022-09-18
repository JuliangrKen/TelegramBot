namespace TelegramBot.ConsoleApp.Extensions
{
    public static class DirectoryExtension
    {
        /// <summary>
        /// Получаем путь до каталога с .sln файлом
        /// </summary>
        public static string GetSolutionRoot()
        {
            var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory()) ?? throw new ArgumentNullException();

            var fullname = (Directory.GetParent(dir) ?? throw new ArgumentNullException()).FullName;
            var projectRoot = fullname[..^4];
            return Directory.GetParent(projectRoot)?.FullName ?? throw new ArgumentNullException();
        }
    }
}