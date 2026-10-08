using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Finance.Services
{
    // Вход, регистрация и текущий пользователь
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

        // true, если логин ещё никем не занят
        public static bool IsLoginFree(string login)
        {
            return !Core.Context.Users.Any(u => u.Login == login);
        }

        // Проверка правил пароля, возвращает текст ошибок или пустую строку
        public static string CheckPassword(string password)
        {
            StringBuilder errors = new StringBuilder();
            if (password.Length < 6)
                errors.AppendLine("Пароль должен быть не короче 6 символов");
            if (!Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
                errors.AppendLine("Пароль может содержать только латинские буквы и цифры");
            if (!Regex.IsMatch(password, "[0-9]"))
                errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
            return errors.ToString();
        }

        // Добавление нового пользователя, в БД пишется только хэш пароля
        public static void Register(string login, string password, string fio, string role)
        {
            Users user = new Users();
            user.Login = login;
            user.Password = PasswordHasher.GetHash(password);
            user.FIO = fio;
            user.Role = role;
            user.Theme = "Light";
            Core.Context.Users.Add(user);
            Core.Context.SaveChanges();
        }
    }
}
