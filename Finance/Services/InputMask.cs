using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Finance.Services
{
    // Ограничения ввода: неподходящий символ просто не попадает в поле.
    // Обработчики подключаются к полям в конструкторах страниц
    public static class InputMask
    {
        // Логин: латинские буквы, цифры и _
        public static void Login(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[a-zA-Z0-9_]+$");
        }

        // ФИО: русские буквы, пробел и дефис
        public static void FIO(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[а-яА-ЯёЁ -]+$");
        }

        // Сумма: цифры и только одна запятая
        public static void Amount(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (e.Text == ",")
                e.Handled = box.Text.Contains(",");
            else
                e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        // Пробел не вызывает PreviewTextInput, поэтому запрещаем его по нажатию клавиши
        public static void NoSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        // Запрет вставки из буфера обмена (Ctrl+V и пункт контекстного меню)
        public static void NoPaste(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }
    }
}
