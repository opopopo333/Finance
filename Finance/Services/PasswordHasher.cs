using System;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Services
{
    // Хэширование паролей
    public static class PasswordHasher
    {
        // SHA-256 от пароля в виде HEX-строки в верхнем регистре
        public static string GetHash(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}
