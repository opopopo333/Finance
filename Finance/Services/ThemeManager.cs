using System;
using System.Windows;

namespace Finance.Services
{
    // Смена светлой и тёмной темы
    public static class ThemeManager
    {
        // Подключает словарь Styles/LightTheme.xaml или Styles/DarkTheme.xaml и запоминает тему в настройках
        public static void Apply(string theme)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("Styles/" + theme + "Theme.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);

            Properties.Settings.Default.Theme = theme;
            Properties.Settings.Default.Save();
        }
    }
}
