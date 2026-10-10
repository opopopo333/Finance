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

            UpdateOperations();
        }

        // Операции текущего пользователя, новые сверху
        private void UpdateOperations()
        {
            int userId = AuthService.CurrentUser.ID;
            List<Operation> operations = Core.Context.Operation.Where(o => o.UserID == userId)
                .OrderByDescending(o => o.Date).ToList();

            DGridOperations.ItemsSource = operations;

            // Итоги считаются только по операциям, которые сейчас в таблице
            decimal income = operations.Where(o => o.Category.Type == "Доход").Sum(o => o.Amount);
            decimal expense = operations.Where(o => o.Category.Type == "Расход").Sum(o => o.Amount);
            TBlockTotals.Text = "Доходы: " + income.ToString("N2") + "     Расходы: " + expense.ToString("N2")
                + "     Баланс: " + (income - expense).ToString("N2");
        }
    }
}
