namespace TelegramBot.ConsoleApp.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Преобразуем строку, чтобы она начиналась с заглавной буквы
        /// </summary>
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return char.ToUpper(s[0]) + s[1..];
        }
    }
}