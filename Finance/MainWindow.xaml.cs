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
using System.Windows.Threading;
using Finance.Pages;
using Finance.Services;

namespace Finance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // До входа используется последняя выбранная тема
            ThemeManager.Apply(Properties.Settings.Default.Theme);

            // Часы внизу окна обновляются раз в секунду
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            MainFrame.Navigate(new AuthPage());
        }

        // Вывод текущих даты и времени
        private void Timer_Tick(object sender, EventArgs e)
        {
            TBlockDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        // Возврат на предыдущую страницу
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        // Переключение светлой и тёмной темы
        private void BtnTheme_Click(object sender, RoutedEventArgs e)
        {
            string theme = "Light";
            if (Properties.Settings.Default.Theme == "Light")
                theme = "Dark";
            ThemeManager.Apply(theme);

            // Диаграмму из Windows Forms стили WPF не перекрашивают, делаем это вручную
            if (MainFrame.Content is DiagramPage)
                ((DiagramPage)MainFrame.Content).ApplyTheme();

            // У вошедшего пользователя тема сохраняется в БД
            if (AuthService.CurrentUser != null)
            {
                AuthService.CurrentUser.Theme = theme;
                Core.Context.SaveChanges();
            }
        }

        // Выход из аккаунта на страницу авторизации
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (!Msg.Confirm("Вы действительно хотите выйти из аккаунта?"))
                return;
            AuthService.CurrentUser = null;
            MainFrame.Navigate(new AuthPage());
        }

        // Заголовок окна берётся из заголовка открытой страницы
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Title = ((Page)e.Content).Title;
            TBlockTitle.Text = Title;

            // После входа и выхода история очищается, чтобы «Назад» не вернул на чужую страницу
            if (e.Content is AuthPage || e.Content is AdminPage || e.Content is UserPage)
            {
                while (MainFrame.CanGoBack)
                    MainFrame.RemoveBackEntry();
            }
        }

        // Кнопка «Назад» видна, только если есть куда возвращаться, «Выйти» — только после входа
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
                BtnBack.Visibility = Visibility.Visible;
            else
                BtnBack.Visibility = Visibility.Hidden;

            if (AuthService.CurrentUser != null)
                BtnLogout.Visibility = Visibility.Visible;
            else
                BtnLogout.Visibility = Visibility.Hidden;
        }

        // Подтверждение при закрытии окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Msg.Confirm("Вы действительно хотите выйти?"))
                e.Cancel = true;
        }
    }
}
