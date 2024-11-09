using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        public SettingsWindow()
        {
            InitializeComponent();
            SetCurrentLanguageSelection();
        }

        private void SetCurrentLanguageSelection()
        {
            // Получаем текущую культуру из настроек
            var currentCulture = Settings.Default.AppCulture ?? "en-US";
 
            // Проходим по элементам ComboBox и устанавливаем текущий язык
            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Tag.ToString() == currentCulture)
                {
                    LanguageComboBox.SelectedItem = item;
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
        }
    }
}
