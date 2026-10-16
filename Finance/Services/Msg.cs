using System.Windows;

namespace Finance.Services
{
    // Все сообщения пользователю выводятся через этот класс.
    // Владелец сообщения — главное окно, иначе после работы Word окно сообщения может открыться позади программы
    public static class Msg
    {
        // Информационное сообщение
        public static void Info(string text)
        {
            MessageBox.Show(Application.Current.MainWindow, text, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Предупреждение (например, неверный ввод)
        public static void Warning(string text)
        {
            MessageBox.Show(Application.Current.MainWindow, text, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Сообщение об ошибке
        public static void Error(string text)
        {
            MessageBox.Show(Application.Current.MainWindow, text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Вопрос «Да/Нет», возвращает true, если нажали «Да»
        public static bool Confirm(string text)
        {
            return MessageBox.Show(Application.Current.MainWindow, text, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
