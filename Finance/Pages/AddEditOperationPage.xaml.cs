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
    /// Interaction logic for AddEditOperationPage.xaml
    /// </summary>
    public partial class AddEditOperationPage : Page
    {
        // Редактируемая операция, null при добавлении
        private Operation currentOperation;

        public AddEditOperationPage(Operation operation)
        {
            InitializeComponent();
            currentOperation = operation;

            // В сумму только цифры и одна запятая, вставка из буфера запрещена
            TBoxAmount.PreviewTextInput += InputMask.Amount;
            TBoxAmount.PreviewKeyDown += InputMask.NoSpace;
            CommandManager.AddPreviewExecutedHandler(TBoxAmount, InputMask.NoPaste);

            // Администратор выбирает пользователя, обычный пользователь ведёт только свои операции
            if (AuthService.CurrentUser.Role == "Admin")
                CBoxUser.ItemsSource = Core.Context.Users.OrderBy(u => u.Login).ToList();
            else
            {
                CBoxUser.ItemsSource = new List<Users> { AuthService.CurrentUser };
                CBoxUser.SelectedIndex = 0;
                LblUser.Visibility = Visibility.Collapsed;
                CBoxUser.Visibility = Visibility.Collapsed;
            }
            CBoxCategory.ItemsSource = Core.Context.Category.OrderBy(c => c.Name).ToList();

            if (operation == null)
                DPickerDate.SelectedDate = DateTime.Today;
            else
            {
                Title = "Редактирование операции";
                CBoxUser.SelectedItem = operation.Users;
                CBoxCategory.SelectedItem = operation.Category;
                DPickerDate.SelectedDate = operation.Date;
                TBoxName.Text = operation.Name;
                TBoxAmount.Text = operation.Amount.ToString();
                TBoxComment.Text = operation.Comment;
            }
        }

        // Проверка полей и сохранение операции
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = TBoxName.Text.Trim();
            string comment = TBoxComment.Text.Trim();

            StringBuilder errors = new StringBuilder();
            if (CBoxUser.SelectedItem == null)
                errors.AppendLine("Выберите пользователя");
            if (CBoxCategory.SelectedItem == null)
                errors.AppendLine("Выберите категорию");
            if (DPickerDate.SelectedDate == null)
                errors.AppendLine("Выберите дату");
            else if (DPickerDate.SelectedDate > DateTime.Today)
                errors.AppendLine("Дата не может быть позже сегодняшней");
            if (name == "")
                errors.AppendLine("Введите название");
            decimal amount;
            if (!decimal.TryParse(TBoxAmount.Text, out amount))
                errors.AppendLine("Сумма должна быть числом, например 1500,50");
            else if (amount < 0.01m || amount > 10000000)
                errors.AppendLine("Сумма должна быть от 0,01 до 10 000 000");
            else if (amount != Math.Round(amount, 2))
                errors.AppendLine("В сумме не больше двух знаков после запятой");
            if (errors.Length > 0)
            {
                Msg.Warning(errors.ToString());
                return;
            }

            if (currentOperation == null)
            {
                currentOperation = new Operation();
                Core.Context.Operation.Add(currentOperation);
            }
            currentOperation.Users = (Users)CBoxUser.SelectedItem;
            currentOperation.Category = (Category)CBoxCategory.SelectedItem;
            currentOperation.Date = DPickerDate.SelectedDate.Value;
            currentOperation.Name = name;
            currentOperation.Amount = amount;
            // Пустой комментарий хранится в БД как NULL
            if (comment == "")
                currentOperation.Comment = null;
            else
                currentOperation.Comment = comment;
            Core.Context.SaveChanges();

            Msg.Info("Данные сохранены");
            NavigationService.GoBack();
        }
    }
}
