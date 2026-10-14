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
    /// Interaction logic for OperationTabPage.xaml
    /// </summary>
    public partial class OperationTabPage : Page
    {
        public OperationTabPage()
        {
            InitializeComponent();

            // Логины для фильтра, первый пункт «Все»
            List<string> logins = new List<string>();
            logins.Add("Все");
            logins.AddRange(Core.Context.Users.OrderBy(u => u.Login).Select(u => u.Login));
            CBoxUser.ItemsSource = logins;

            CBoxUser.SelectedIndex = 0;
            CBoxSort.SelectedIndex = 0;
        }

        // Операции всех пользователей с учётом поиска, фильтра и сортировки
        private void UpdateOperations()
        {
            List<Operation> operations = Core.Context.Operation.ToList();

            // Поиск без учёта регистра
            string search = TBoxSearch.Text.Trim().ToLower();
            if (search != "")
                operations = operations.Where(o => o.Name.ToLower().Contains(search)
                    || (o.Comment != null && o.Comment.ToLower().Contains(search))).ToList();

            if (CBoxUser.SelectedIndex > 0)
            {
                string login = CBoxUser.SelectedItem.ToString();
                operations = operations.Where(o => o.Users.Login == login).ToList();
            }

            if (CBoxSort.SelectedIndex == 0)
                operations = operations.OrderByDescending(o => o.Date).ToList();
            else if (CBoxSort.SelectedIndex == 1)
                operations = operations.OrderBy(o => o.Date).ToList();
            else if (CBoxSort.SelectedIndex == 2)
                operations = operations.OrderByDescending(o => o.Amount).ToList();
            else if (CBoxSort.SelectedIndex == 3)
                operations = operations.OrderBy(o => o.Amount).ToList();
            else if (CBoxSort.SelectedIndex == 4)
                operations = operations.OrderBy(o => o.Name).ToList();

            DGridOperations.ItemsSource = operations;
        }

        // При каждом показе страницы данные перечитываются из БД
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                Core.Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                UpdateOperations();
            }
        }

        // Поиск на лету при вводе текста
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOperations();
        }

        // Изменение фильтра или сортировки
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOperations();
        }

        // Добавление: страница открывается без операции
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditOperationPage(null));
        }

        // Редактирование операции из строки, где нажата кнопка
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Operation operation = (Operation)((Button)sender).DataContext;
            NavigationService.Navigate(new AddEditOperationPage(operation));
        }

        // Удаление выбранных операций
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Operation> operations = DGridOperations.SelectedItems.Cast<Operation>().ToList();
            if (operations.Count == 0)
            {
                Msg.Warning("Выберите операции для удаления");
                return;
            }
            if (!Msg.Confirm("Удалить выбранные операции (" + operations.Count + ")?"))
                return;

            Core.Context.Operation.RemoveRange(operations);
            Core.Context.SaveChanges();
            Msg.Info("Операции удалены");
            UpdateOperations();
        }
    }
}
