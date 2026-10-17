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
using Microsoft.Win32;
using Finance.Services;

namespace Finance.Pages
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        // Окно выбора файла, возвращает путь или null, если пользователь нажал «Отмена»
        private string GetSavePath(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.FileName = "Отчёт по пользователям";
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        // Выгрузка в Word (.docx)
        private void BtnWord_Click(object sender, RoutedEventArgs e)
        {
            string path = GetSavePath("Документ Word (*.docx)|*.docx");
            if (path == null)
                return;
            try
            {
                ExportService.ExportWord(path, false);
                Msg.Info("Отчёт сохранён: " + path);
            }
            catch (Exception ex)
            {
                Msg.Error("Не удалось создать отчёт. Возможно, не установлен Microsoft Word.\n" + ex.Message);
            }
        }

        // Тот же документ Word, сохранённый в PDF
        private void BtnPdf_Click(object sender, RoutedEventArgs e)
        {
            string path = GetSavePath("Документ PDF (*.pdf)|*.pdf");
            if (path == null)
                return;
            try
            {
                ExportService.ExportWord(path, true);
                Msg.Info("Отчёт сохранён: " + path);
            }
            catch (Exception ex)
            {
                Msg.Error("Не удалось создать отчёт. Возможно, не установлен Microsoft Word.\n" + ex.Message);
            }
        }

        // Выгрузка в Excel (.xlsx)
        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            string path = GetSavePath("Книга Excel (*.xlsx)|*.xlsx");
            if (path == null)
                return;
            try
            {
                ExportService.ExportExcel(path);
                Msg.Info("Отчёт сохранён: " + path);
            }
            catch (Exception ex)
            {
                Msg.Error("Не удалось создать отчёт. Возможно, не установлен Microsoft Excel.\n" + ex.Message);
            }
        }
    }
}
