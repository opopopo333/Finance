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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();

            // Категории для фильтра, первый пункт «Все»
            List<string> categories = new List<string>();
            categories.Add("Все");
            categories.AddRange(Core.Context.Category.OrderBy(c => c.Name).Select(c => c.Name));
            CBoxCategory.ItemsSource = categories;

            ResetFilters();
        }

        // Операции текущего пользователя с учётом поиска, фильтров и сортировки
        private void UpdateOperations()
        {
            int userId = AuthService.CurrentUser.ID;
            List<Operation> operations = Core.Context.Operation.Where(o => o.UserID == userId).ToList();

            // Поиск без учёта регистра
            string search = TBoxSearch.Text.Trim().ToLower();
            if (search != "")
                operations = operations.Where(o => o.Name.ToLower().Contains(search)
                    || (o.Comment != null && o.Comment.ToLower().Contains(search))).ToList();

            if (CBoxCategory.SelectedIndex > 0)
            {
                string category = CBoxCategory.SelectedItem.ToString();
                operations = operations.Where(o => o.Category.Name == category).ToList();
            }

            if (CBoxType.SelectedIndex > 0)
            {
                string type = ((ComboBoxItem)CBoxType.SelectedItem).Content.ToString();
                operations = operations.Where(o => o.Category.Type == type).ToList();
            }

            if (DPickerFrom.SelectedDate != null)
                operations = operations.Where(o => o.Date >= DPickerFrom.SelectedDate).ToList();
            if (DPickerTo.SelectedDate != null)
                operations = operations.Where(o => o.Date <= DPickerTo.SelectedDate).ToList();

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

            // Итоги считаются только по операциям, которые сейчас в таблице
            decimal income = operations.Where(o => o.Category.Type == "Доход").Sum(o => o.Amount);
            decimal expense = operations.Where(o => o.Category.Type == "Расход").Sum(o => o.Amount);
            TBlockTotals.Text = "Доходы: " + income.ToString("N2") + "     Расходы: " + expense.ToString("N2")
                + "     Баланс: " + (income - expense).ToString("N2");
        }

        // Возврат поиска, фильтров и сортировки к начальным значениям
        private void ResetFilters()
        {
            TBoxSearch.Text = "";
            CBoxCategory.SelectedIndex = 0;
            CBoxType.SelectedIndex = 0;
            DPickerFrom.SelectedDate = null;
            DPickerTo.SelectedDate = null;
            CBoxSort.SelectedIndex = 0;
            UpdateOperations();
        }

        // Поиск на лету при вводе текста
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOperations();
        }

        // Изменение любого списка или даты
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOperations();
        }

        // Кнопка «Сбросить фильтры»
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFilters();
        }
    }
}
