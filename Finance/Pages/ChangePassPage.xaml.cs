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
    /// Interaction logic for ChangePassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        public ChangePassPage()
        {
            InitializeComponent();

            // Ограничения ввода
            TBoxLogin.PreviewTextInput += InputMask.Login;
            TBoxLogin.PreviewKeyDown += InputMask.NoSpace;
        }

        // Смена пароля: проверка полей, сохранение и возврат на авторизацию
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text.Trim();

            if (login == "" || PBoxOld.Password == "" || PBoxNew.Password == "" || PBoxConfirm.Password == "")
            {
                Msg.Warning("Заполните все поля");
                return;
            }

            // Для нового пароля те же правила, что и при регистрации
            StringBuilder errors = new StringBuilder();
            errors.Append(AuthService.CheckPassword(PBoxNew.Password));
            if (PBoxNew.Password != PBoxConfirm.Password)
                errors.AppendLine("Пароли не совпадают");
            if (PBoxNew.Password == PBoxOld.Password)
                errors.AppendLine("Новый пароль должен отличаться от текущего");
            if (errors.Length > 0)
            {
                Msg.Warning(errors.ToString());
                return;
            }

            if (!AuthService.ChangePassword(login, PBoxOld.Password, PBoxNew.Password))
            {
                Msg.Warning("Неверный логин или текущий пароль");
                return;
            }

            Msg.Info("Пароль изменён");
            NavigationService.Navigate(new AuthPage());
        }
    }
}
