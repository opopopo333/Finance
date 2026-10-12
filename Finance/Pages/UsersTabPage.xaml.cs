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
    /// Interaction logic for UsersTabPage.xaml
    /// </summary>
    public partial class UsersTabPage : Page
    {
        public UsersTabPage()
        {
            InitializeComponent();
            CBoxRole.SelectedIndex = 0;
            CBoxSort.SelectedIndex = 0;
        }

        // Пользователи с учётом поиска, фильтра и сортировки
        private void UpdateUsers()
        {
            List<Users> users = Core.Context.Users.ToList();

            string search = TBoxSearch.Text.Trim().ToLower();
            if (search != "")
                users = users.Where(u => u.Login.ToLower().Contains(search) || u.FIO.ToLower().Contains(search)).ToList();

            if (CBoxRole.SelectedIndex > 0)
            {
                string role = ((ComboBoxItem)CBoxRole.SelectedItem).Tag.ToString();
                users = users.Where(u => u.Role == role).ToList();
            }

            if (CBoxSort.SelectedIndex == 0)
                users = users.OrderBy(u => u.Login).ToList();
            else if (CBoxSort.SelectedIndex == 1)
                users = users.OrderByDescending(u => u.Login).ToList();
            else if (CBoxSort.SelectedIndex == 2)
                users = users.OrderBy(u => u.FIO).ToList();

            DGridUsers.ItemsSource = users;
        }

        // При каждом показе страницы данные перечитываются из БД
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                Core.Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                UpdateUsers();
            }
        }

        // Поиск на лету при вводе текста
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        // Изменение фильтра или сортировки
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        // Добавление: страница открывается без пользователя
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditUserPage(null));
        }

        // Редактирование пользователя из строки, где нажата кнопка
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Users user = (Users)((Button)sender).DataContext;
            NavigationService.Navigate(new AddEditUserPage(user));
        }

        // Удаление выбранных пользователей вместе с их операциями
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Users> users = DGridUsers.SelectedItems.Cast<Users>().ToList();
            if (users.Count == 0)
            {
                Msg.Warning("Выберите пользователей для удаления");
                return;
            }
            if (users.Contains(AuthService.CurrentUser))
            {
                Msg.Warning("Нельзя удалить самого себя");
                return;
            }
            if (!Msg.Confirm("Удалить выбранных пользователей (" + users.Count + ")?\nБудут удалены и все их операции."))
                return;

            Core.Context.Users.RemoveRange(users);
            Core.Context.SaveChanges();
            Msg.Info("Пользователи удалены");
            UpdateUsers();
        }
    }
}
