using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Finance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Один обработчик на все поля пароля в программе
            EventManager.RegisterClassHandler(typeof(PasswordBox), PasswordBox.PasswordChangedEvent,
                new RoutedEventHandler(PasswordBox_PasswordChanged));
        }

        // У PasswordBox нет свойства, к которому можно привязать подсказку, поэтому прячем её из кода.
        // В Tag поля пароля лежит его подсказка — TextBlock поверх поля
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = (PasswordBox)sender;
            TextBlock hint = box.Tag as TextBlock;
            if (hint == null)
                return;
            if (box.Password == "")
                hint.Visibility = Visibility.Visible;
            else
                hint.Visibility = Visibility.Collapsed;
        }
    }
}
