using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace NoteBookUI
{
    public partial class SettingsWindow : Window
    {

        public SettingsWindow()
        {
            InitializeComponent();
            SetCurrentLanguageSelection();
            SetCurrentIntervalSelection();
        }

        private void SetCurrentLanguageSelection()
        {
            // Получаем текущую культуру из настроек
            var currentLanguage = Settings.Default.AppCulture ?? "en-US";
 
            // Проходим по элементам ComboBox и устанавливаем текущий язык
            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Tag.ToString() == currentLanguage)
                {
                    LanguageComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void SetCurrentIntervalSelection()
        {
            // Получаем текущее значение интервала из настроек
            var currentInterval = Settings.Default.AutoSaveInterval ?? "No";

            // Проходим по элементам ComboBox и устанавливаем текущий интервал
            foreach (ComboBoxItem item in AutoSaveIntervalComboBox.Items)
            {
                if (item.Tag.ToString() == currentInterval)
                {
                    AutoSaveIntervalComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is ComboBoxItem selectedItem && 
                    selectedItem.Tag is string cultureName)
            {
                // Меняем культуру приложения
                ((App)Application.Current).ChangeCulture(cultureName);

                // Сохраняем выбранную культуру в настройках
                Settings.Default.AppCulture = cultureName;
                Settings.Default.Save();

            }

            if (AutoSaveIntervalComboBox.SelectedItem is ComboBoxItem selectedInterval &&
                    selectedInterval.Tag is string interval)
            {

                // Сохраняем выбранную культуру в настройках
                Settings.Default.AutoSaveInterval = interval;
                Settings.Default.Save();

                // Меняем культуру приложения
                ((App)Application.Current).ChangeInterval();

            }
        }
    }
}
