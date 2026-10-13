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
    /// Interaction logic for AddEditCategoryPage.xaml
    /// </summary>
    public partial class AddEditCategoryPage : Page
    {
        // Редактируемая категория, null при добавлении
        private Category currentCategory;

        public AddEditCategoryPage(Category category)
        {
            InitializeComponent();
            currentCategory = category;

            if (category != null)
            {
                Title = "Редактирование категории";
                TBoxName.Text = category.Name;
                if (category.Type == "Расход")
                    CBoxType.SelectedIndex = 1;
            }
        }

        // Проверка полей и сохранение категории
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = TBoxName.Text.Trim();
            string type = ((ComboBoxItem)CBoxType.SelectedItem).Content.ToString();

            // ID текущей категории, чтобы при проверке названия не найти её саму
            int id = 0;
            if (currentCategory != null)
                id = currentCategory.ID;

            StringBuilder errors = new StringBuilder();
            if (name == "")
                errors.AppendLine("Введите название");
            else if (Core.Context.Category.Any(c => c.Name == name && c.ID != id))
                errors.AppendLine("Категория с таким названием уже существует");
            if (errors.Length > 0)
            {
                Msg.Warning(errors.ToString());
                return;
            }

            if (currentCategory == null)
            {
                currentCategory = new Category();
                Core.Context.Category.Add(currentCategory);
            }
            currentCategory.Name = name;
            currentCategory.Type = type;
            Core.Context.SaveChanges();

            Msg.Info("Данные сохранены");
            NavigationService.GoBack();
        }
    }
}
