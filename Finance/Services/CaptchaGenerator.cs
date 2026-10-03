using System;

namespace Finance.Services
{
    // Генерация капчи
    public static class CaptchaGenerator
    {
        private static Random random = new Random();

        // Без похожих символов (0 и O, 1 и l), чтобы капчу можно было прочитать
        private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";

        // Случайная строка из латинских букв и цифр
        public static string Generate(int length = 6)
        {
            string result = "";
            for (int i = 0; i < length; i++)
                result += Chars[random.Next(Chars.Length)];
            return result;
        }
    }
}
