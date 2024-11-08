using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using NoteBookLib;


namespace NoteBookUI.View
{
    public class MainViewModel : OnPropertyChangedHandler
    {

        private ObservableCollection<TabItemExtended> _tabs;

        public MainViewModel()
        {
            _tabs = [];

        }

        public ObservableCollection<TabItemExtended> getTabsList() => _tabs;

        public void CreateNewTab()
        {
            var tabTextEditor = new TextEditor();
            tabTextEditor.CreateFile();
            var newTab = new TabItemExtended(tabTextEditor);
            _tabs.Add(newTab);
        }

        public void CloseTab(TabItemExtended tab)
        {
            if (tab == null)
                return;

            if (!tab.CanRemoveTab())
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

            _tabs.Remove(tab);

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
 
                _tabs.Add(newTab);
            }
        }

        public void SaveFile(TabItemExtended tab)
        {
            if (tab == null)
                return;

            if (tab.isNewFile())
            {
                SaveFileAs(tab);
                return;
            }

            tab.Save();
        }

        public void SaveFileAs(TabItemExtended tab)
        {
            if (tab == null)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                
                tab.Save(filePath);
            }
        }
    }
}
