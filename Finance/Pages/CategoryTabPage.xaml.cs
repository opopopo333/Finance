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
    /// Interaction logic for CategoryTabPage.xaml
    /// </summary>
    public partial class CategoryTabPage : Page
    {
        public CategoryTabPage()
        {
            InitializeComponent();
            CBoxType.SelectedIndex = 0;
            CBoxSort.SelectedIndex = 0;
        }

        // Категории с учётом поиска, фильтра и сортировки
        private void UpdateCategories()
        {
            List<Category> categories = Core.Context.Category.ToList();

            string search = TBoxSearch.Text.Trim().ToLower();
            if (search != "")
                categories = categories.Where(c => c.Name.ToLower().Contains(search)).ToList();

            if (CBoxType.SelectedIndex > 0)
            {
                string type = ((ComboBoxItem)CBoxType.SelectedItem).Content.ToString();
                categories = categories.Where(c => c.Type == type).ToList();
            }

            if (CBoxSort.SelectedIndex == 0)
                categories = categories.OrderBy(c => c.Name).ToList();
            else if (CBoxSort.SelectedIndex == 1)
                categories = categories.OrderByDescending(c => c.Name).ToList();

            DGridCategories.ItemsSource = categories;
        }

        // При каждом показе страницы данные перечитываются из БД
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                Core.Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                UpdateCategories();
            }
        }

        // Поиск на лету при вводе текста
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCategories();
        }

        // Изменение фильтра или сортировки
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCategories();
        }

        // Добавление: страница открывается без категории
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditCategoryPage(null));
        }

        // Редактирование категории из строки, где нажата кнопка
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Category category = (Category)((Button)sender).DataContext;
            NavigationService.Navigate(new AddEditCategoryPage(category));
        }

        // Удаление выбранных категорий вместе с их операциями
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Category> categories = DGridCategories.SelectedItems.Cast<Category>().ToList();
            if (categories.Count == 0)
            {
                Msg.Warning("Выберите категории для удаления");
                return;
            }
            if (!Msg.Confirm("Удалить выбранные категории (" + categories.Count + ")?\nБудут удалены и все операции в этих категориях."))
                return;

            Core.Context.Category.RemoveRange(categories);
            Core.Context.SaveChanges();
            Msg.Info("Категории удалены");
            UpdateCategories();
        }
    }
}
