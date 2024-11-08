using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;


namespace NoteBookUI.View
{
    public class MainViewModel : OnPropertyChangedHandler
    {

        public ObservableCollection<TabItemExtended> Tabs;

        public MainViewModel()
        {
            Tabs = [];

        }

        public void CreateNewTab()
        {
            var tabTextEditor = new TextEditor();
            tabTextEditor.CreateFile();
            var newTab = new TabItemExtended(tabTextEditor);
            Tabs.Add(newTab);
        }

        public void CloseTab(TabItemExtended tab)
        {
            if (tab == null)
                return;

            if (!tab.TabViewModel.CanRemove())
            {
                var result = MessageBox.Show(
                        "You have unsaved changes. Do you really want to close this tab?",
                        "Unsaved Changes",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            Tabs.Remove(tab);

        }

        public void OpenExisttingFile()
        {
            // Создаем диалог открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {

                var newTabViewModel = new TextEditor();

                newTabViewModel.LoadFile(openFileDialog.FileName);

                var newTab = new TabItemExtended(newTabViewModel);
 
                Tabs.Add(newTab);
            }
        }
    }
}
