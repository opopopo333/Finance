using System.Linq;

namespace Finance.Services
{
    // Вход в систему и текущий пользователь
    public static class AuthService
    {
        // Пользователь, который сейчас вошёл (null, если никто)
        public static Users CurrentUser { get; set; }

        // Поиск пользователя по логину и хэшу пароля, null если не найден
        public static Users Login(string login, string password)
        {
            string hash = PasswordHasher.GetHash(password);
            CurrentUser = Core.Context.Users.FirstOrDefault(u => u.Login == login && u.Password == hash);
            return CurrentUser;
        }
    }
}
