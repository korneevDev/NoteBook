using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static event Action? LanguageChanged;
        public static event Action? IntervalChanged;

        public void ChangeCulture(string cultureName)
        {
            // Загружаем новый словарь ресурсов
            var dictionary = new ResourceDictionary
            {
                Source = new Uri($"Resources/Strings.{cultureName}.xaml", UriKind.Relative)
            };

            // Обновляем словари ресурсов
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dictionary);

            // Устанавливаем текущую культуру
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureName);

            // Сохраняем выбранную культуру в настройках
            Settings.Default.AppCulture = cultureName;
            Settings.Default.Save();

            LanguageChanged?.Invoke();
        }
        
        public void ChangeCulture()
        {
            var currentCulture = Settings.Default.AppCulture ?? "en-US";
            ChangeCulture(currentCulture);
        }

        public void ChangeInterval()
        {
            IntervalChanged?.Invoke();
        }
    }

}
