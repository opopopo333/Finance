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

namespace Finance.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        // Переход к таблице пользователей
        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersTabPage());
        }

        // Переход к таблице категорий
        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryTabPage());
        }

        // Переход к таблице операций
        private void BtnOperations_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OperationTabPage());
        }

        // Переход к диаграмме
        private void BtnDiagram_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DiagramPage());
        }
    }
}
