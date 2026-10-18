using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Finance.Services;

namespace Finance.Pages
{
    /// <summary>
    /// Interaction logic for RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();

            // Ограничения ввода
            TBoxLogin.PreviewTextInput += InputMask.Login;
            TBoxLogin.PreviewKeyDown += InputMask.NoSpace;
            TBoxFIO.PreviewTextInput += InputMask.FIO;
        }

        // Регистрация: проверка полей и сохранение нового пользователя
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text.Trim();
            string fio = TBoxFIO.Text.Trim();

            if (login == "" || PBoxPassword.Password == "" || PBoxConfirm.Password == "" || fio == "")
            {
                Msg.Warning("Заполните все поля");
                return;
            }

            // Все ошибки собираются и выводятся одним сообщением
            StringBuilder errors = new StringBuilder();
            errors.Append(AuthService.CheckLogin(login));
            if (!AuthService.IsLoginFree(login))
                errors.AppendLine("Пользователь с таким логином уже существует");
            errors.Append(AuthService.CheckFIO(fio));
            errors.Append(AuthService.CheckPassword(PBoxPassword.Password));
            if (PBoxPassword.Password != PBoxConfirm.Password)
                errors.AppendLine("Пароли не совпадают");
            if (errors.Length > 0)
            {
                Msg.Warning(errors.ToString());
                return;
            }

            string role = ((ComboBoxItem)CBoxRole.SelectedItem).Tag.ToString();
            AuthService.Register(login, PBoxPassword.Password, fio, role);
            Msg.Info("Пользователь зарегистрирован");

            // Очистка формы
            TBoxLogin.Text = "";
            PBoxPassword.Password = "";
            PBoxConfirm.Password = "";
            TBoxFIO.Text = "";
            CBoxRole.SelectedIndex = 0;
        }
    }
}
