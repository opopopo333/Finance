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
    /// Interaction logic for AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {
        // Редактируемый пользователь, null при добавлении
        private Users currentUser;

        public AddEditUserPage(Users user)
        {
            InitializeComponent();
            currentUser = user;

            if (user != null)
            {
                Title = "Редактирование пользователя";
                LblPassword.Content = "Новый пароль (пустой — не менять)";
                TBoxLogin.Text = user.Login;
                TBoxFIO.Text = user.FIO;
                if (user.Role == "Admin")
                    CBoxRole.SelectedIndex = 1;
            }
        }

        // Проверка полей и сохранение пользователя
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text.Trim();
            string password = PBoxPassword.Password;
            string fio = TBoxFIO.Text.Trim();
            string role = ((ComboBoxItem)CBoxRole.SelectedItem).Tag.ToString();

            // ID текущего пользователя, чтобы при проверке логина не найти самого себя
            int id = 0;
            if (currentUser != null)
                id = currentUser.ID;

            StringBuilder errors = new StringBuilder();
            if (login == "")
                errors.AppendLine("Введите логин");
            else if (Core.Context.Users.Any(u => u.Login == login && u.ID != id))
                errors.AppendLine("Пользователь с таким логином уже существует");
            if (fio == "")
                errors.AppendLine("Введите ФИО");
            // При редактировании пустой пароль значит «не менять»
            if (currentUser == null || password != "")
                errors.Append(AuthService.CheckPassword(password));
            if (errors.Length > 0)
            {
                Msg.Warning(errors.ToString());
                return;
            }

            if (currentUser == null)
                AuthService.Register(login, password, fio, role);
            else
            {
                currentUser.Login = login;
                currentUser.FIO = fio;
                currentUser.Role = role;
                if (password != "")
                    currentUser.Password = PasswordHasher.GetHash(password);
                Core.Context.SaveChanges();
            }

            Msg.Info("Данные сохранены");
            NavigationService.GoBack();
        }
    }
}
