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
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        // Количество неверных попыток входа подряд
        private int failCount = 0;

        public AuthPage()
        {
            InitializeComponent();

            // Ограничения ввода
            TBoxLogin.PreviewTextInput += InputMask.Login;
            TBoxLogin.PreviewKeyDown += InputMask.NoSpace;
        }

        // Вход: проверка полей, поиск пользователя и переход по роли
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxLogin.Text) || string.IsNullOrEmpty(PBoxPassword.Password))
            {
                Msg.Warning("Введите логин и пароль");
                return;
            }

            Users user = AuthService.Login(TBoxLogin.Text.Trim(), PBoxPassword.Password);
            if (user == null)
            {
                failCount++;
                if (failCount >= 3)
                {
                    Msg.Warning("Три неверные попытки входа. Введите капчу");
                    ShowCaptcha();
                }
                else
                    Msg.Warning("Неверный логин или пароль");
                return;
            }

            failCount = 0;
            ThemeManager.Apply(user.Theme);
            if (user.Role == "Admin")
                NavigationService.Navigate(new AdminPage());
            else
                NavigationService.Navigate(new UserPage());
        }

        // Показ новой капчи вместо формы входа
        private void ShowCaptcha()
        {
            TBoxCaptcha.Text = CaptchaGenerator.Generate();
            TBoxCaptchaInput.Text = "";
            SPanelLogin.Visibility = Visibility.Collapsed;
            SPanelCaptcha.Visibility = Visibility.Visible;
        }

        // Верная капча возвращает форму входа, неверная заменяется новой
        private void BtnCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxCaptchaInput.Text == TBoxCaptcha.Text)
            {
                failCount = 0;
                SPanelCaptcha.Visibility = Visibility.Collapsed;
                SPanelLogin.Visibility = Visibility.Visible;
            }
            else
            {
                Msg.Warning("Капча введена неверно, попробуйте ещё раз");
                ShowCaptcha();
            }
        }

        // Запрет копирования, вырезания и вставки в полях капчи
        private void Captcha_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }

        // Переход на регистрацию
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        // Переход на смену пароля
        private void BtnChangePass_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangePassPage());
        }
    }
}
