using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Finance.Services
{
    // Выгрузка отчёта по пользователям в Word, PDF и Excel
    public static class ExportService
    {
        // Отчёт в Word: по каждому пользователю суммы по категориям, самая крупная и самая мелкая операция.
        // При toPdf = true тот же документ сохраняется в PDF
        public static void ExportWord(string path, bool toPdf)
        {
            List<Users> users = Core.Context.Users.OrderBy(u => u.FIO).ToList();
            Word.Application app = new Word.Application();
            try
            {
                Word.Document document = app.Documents.Add();

                for (int i = 0; i < users.Count; i++)
                {
                    Users user = users[i];
                    List<Operation> operations = Core.Context.Operation.Where(o => o.UserID == user.ID).ToList();
                    // Категории, в которых у пользователя есть операции
                    List<Category> categories = operations.Select(o => o.Category).Distinct().OrderBy(c => c.Name).ToList();

                    // Заголовок с ФИО
                    Word.Paragraph titleParagraph = document.Paragraphs.Add();
                    Word.Range titleRange = titleParagraph.Range;
                    titleRange.Text = user.FIO;
                    titleRange.set_Style(Word.WdBuiltinStyle.wdStyleHeading1);
                    titleRange.InsertParagraphAfter();

                    // Таблица «Категория | Сумма»
                    Word.Paragraph tableParagraph = document.Paragraphs.Add();
                    Word.Table table = document.Tables.Add(tableParagraph.Range, categories.Count + 1, 2);
                    table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    table.Cell(1, 1).Range.Text = "Категория";
                    table.Cell(1, 2).Range.Text = "Сумма";
                    table.Rows[1].Range.Bold = 1;
                    for (int j = 0; j < categories.Count; j++)
                    {
                        decimal sum = operations.Where(o => o.CategoryID == categories[j].ID).Sum(o => o.Amount);
                        table.Cell(j + 2, 1).Range.Text = categories[j].Name;
                        table.Cell(j + 2, 2).Range.Text = sum.ToString("N2");
                    }

                    if (operations.Count > 0)
                    {
                        Operation max = operations.OrderByDescending(o => o.Amount).First();
                        Operation min = operations.OrderBy(o => o.Amount).First();

                        Word.Paragraph maxParagraph = document.Paragraphs.Add();
                        maxParagraph.Range.Text = "Самая крупная операция: " + max.Name + ", " + max.Amount.ToString("N2")
                            + " от " + max.Date.ToString("dd.MM.yyyy");
                        maxParagraph.Range.InsertParagraphAfter();

                        Word.Paragraph minParagraph = document.Paragraphs.Add();
                        minParagraph.Range.Text = "Самая мелкая операция: " + min.Name + ", " + min.Amount.ToString("N2")
                            + " от " + min.Date.ToString("dd.MM.yyyy");
                        minParagraph.Range.InsertParagraphAfter();
                    }

                    // Каждый следующий пользователь начинается с новой страницы
                    if (i < users.Count - 1)
                        document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }

                // Номера страниц в нижнем колонтитуле
                document.Sections[1].Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary]
                    .PageNumbers.Add(Word.WdPageNumberAlignment.wdAlignPageNumberCenter);

                if (toPdf)
                    document.ExportAsFixedFormat(path, Word.WdExportFormat.wdExportFormatPDF);
                else
                    document.SaveAs2(path);
            }
            finally
            {
                // Закрываем Word без вопроса о сохранении, даже если была ошибка
                app.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        // Отчёт в Excel: лист на каждого пользователя с его операциями и итогом формулой SUM
        public static void ExportExcel(string path)
        {
            WriteExcel(path);
            // Excel завершается, только когда .NET освободит ссылки на его объекты, поэтому вызываем сборщик мусора
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // Создание и сохранение книги Excel
        private static void WriteExcel(string path)
        {
            List<Users> users = Core.Context.Users.OrderBy(u => u.FIO).ToList();
            Excel.Application app = new Excel.Application();
            try
            {
                // Без окон с вопросами: Excel скрыт, и ответить на них некому
                app.DisplayAlerts = false;

                // В новой книге сразу нужное число листов, настройку Excel потом возвращаем
                int oldSheetsCount = app.SheetsInNewWorkbook;
                app.SheetsInNewWorkbook = users.Count;
                Excel.Workbook workbook = app.Workbooks.Add();
                app.SheetsInNewWorkbook = oldSheetsCount;

                for (int i = 0; i < users.Count; i++)
                {
                    Users user = users[i];
                    List<Operation> operations = Core.Context.Operation.Where(o => o.UserID == user.ID)
                        .OrderBy(o => o.Date).ToList();

                    // Лист называется логином, Excel разрешает не больше 31 символа
                    Excel.Worksheet sheet = (Excel.Worksheet)workbook.Worksheets[i + 1];
                    string sheetName = user.Login;
                    if (sheetName.Length > 31)
                        sheetName = sheetName.Substring(0, 31);
                    sheet.Name = sheetName;

                    sheet.Cells[1, 1] = "Дата";
                    sheet.Cells[1, 2] = "Название";
                    sheet.Cells[1, 3] = "Категория";
                    sheet.Cells[1, 4] = "Тип";
                    sheet.Cells[1, 5] = "Сумма";
                    sheet.Cells[1, 6] = "Комментарий";
                    sheet.Rows[1].Font.Bold = true;

                    int row = 2;
                    foreach (Operation operation in operations)
                    {
                        sheet.Cells[row, 1] = operation.Date;
                        sheet.Cells[row, 2] = operation.Name;
                        sheet.Cells[row, 3] = operation.Category.Name;
                        sheet.Cells[row, 4] = operation.Category.Type;
                        sheet.Cells[row, 5] = (double)operation.Amount;
                        sheet.Cells[row, 6] = operation.Comment;
                        row++;
                    }

                    // Итоговая строка формулой
                    sheet.Cells[row, 4] = "Итого";
                    sheet.Cells[row, 5].Formula = "=SUM(E2:E" + (row - 1) + ")";
                    sheet.Rows[row].Font.Bold = true;

                    sheet.Columns.AutoFit();
                }

                workbook.SaveAs(path);
                workbook.Close();
            }
            finally
            {
                app.Quit();
            }
        }
    }
}
