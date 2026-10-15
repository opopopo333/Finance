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
using System.Windows.Forms.DataVisualization.Charting;

namespace Finance.Pages
{
    /// <summary>
    /// Interaction logic for DiagramPage.xaml
    /// </summary>
    public partial class DiagramPage : Page
    {
        public DiagramPage()
        {
            InitializeComponent();

            // Область построения, легенда и одна серия с подписями значений
            ChartOperations.ChartAreas.Add(new ChartArea("Main"));
            ChartOperations.ChartAreas[0].AxisX.Interval = 1;
            ChartOperations.Legends.Add(new Legend("Main"));
            Series series = new Series("Сумма");
            series.IsValueShownAsLabel = true;
            ChartOperations.Series.Add(series);
            ApplyTheme();

            CBoxUser.ItemsSource = Core.Context.Users.OrderBy(u => u.FIO).ToList();
            CBoxChartType.ItemsSource = new List<SeriesChartType>
            {
                SeriesChartType.Column, SeriesChartType.Bar, SeriesChartType.Pie,
                SeriesChartType.Doughnut, SeriesChartType.Line, SeriesChartType.Area
            };

            CBoxUser.SelectedIndex = 0;
            CBoxChartType.SelectedIndex = 0;
            CBoxType.SelectedIndex = 1;
        }

        // Суммы операций выбранного пользователя по категориям
        private void UpdateChart()
        {
            if (CBoxUser.SelectedItem == null || CBoxChartType.SelectedItem == null || CBoxType.SelectedItem == null)
                return;

            Users user = (Users)CBoxUser.SelectedItem;
            string type = ((ComboBoxItem)CBoxType.SelectedItem).Content.ToString();
            List<Operation> operations = Core.Context.Operation.Where(o => o.UserID == user.ID && o.Category.Type == type).ToList();

            Series series = ChartOperations.Series[0];
            series.ChartType = (SeriesChartType)CBoxChartType.SelectedItem;
            series.Points.Clear();

            // Одна точка на каждую категорию, где у пользователя есть операции
            List<Category> categories = Core.Context.Category.Where(c => c.Type == type).OrderBy(c => c.Name).ToList();
            foreach (Category category in categories)
            {
                decimal sum = operations.Where(o => o.CategoryID == category.ID).Sum(o => o.Amount);
                if (sum > 0)
                    series.Points.AddXY(category.Name, sum);
            }
        }

        // Chart из Windows Forms не видит стили WPF, поэтому цвета темы задаются ему вручную
        public void ApplyTheme()
        {
            System.Drawing.Color back = GetThemeColor("BackgroundBrush");
            System.Drawing.Color text = GetThemeColor("TextBrush");

            ChartOperations.BackColor = back;
            ChartArea area = ChartOperations.ChartAreas[0];
            area.BackColor = back;
            area.AxisX.LineColor = text;
            area.AxisY.LineColor = text;
            area.AxisX.LabelStyle.ForeColor = text;
            area.AxisY.LabelStyle.ForeColor = text;
            ChartOperations.Legends[0].BackColor = back;
            ChartOperations.Legends[0].ForeColor = text;
            ChartOperations.Series[0].LabelForeColor = text;
        }

        // Цвет кисти из текущей темы в формате Windows Forms
        private System.Drawing.Color GetThemeColor(string key)
        {
            Color color = ((SolidColorBrush)Application.Current.Resources[key]).Color;
            return System.Drawing.Color.FromArgb(color.R, color.G, color.B);
        }

        // Любое изменение выбора перестраивает диаграмму
        private void Chart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
    }
}
