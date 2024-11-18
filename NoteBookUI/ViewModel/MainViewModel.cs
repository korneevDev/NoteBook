using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using NoteBookLib;
using NoteBookUI.Utils;
using System.Windows.Controls;

namespace NoteBookUI.View
{
    public class MainViewModel : OnPropertyChangedHandler
    {

        private readonly ObservableCollection<TabItemExtended> _tabs;
        private readonly ClipboardManager _clipboardManager;

        public MainViewModel()
        {
            _tabs = [];
            _clipboardManager = new ClipboardManager();
            App.LanguageChanged += UpdateTabTitles;
        }

        private void UpdateTabTitles()
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateTitle();
            }

        }

        public ObservableCollection<TabItemExtended> GetTabsList() => _tabs;

        public void CreateNewTab()
        {
            var tabTextEditor = new TextEditor(_clipboardManager);
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
                        StringResourceManager.GetString("UnsavedChangesMessage"),
                        StringResourceManager.GetString("UnsavedChangesLabel"),
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
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {

                var newTabViewModel = new TextEditor(_clipboardManager);

                newTabViewModel.LoadFile(openFileDialog.FileName);

                var newTab = new TabItemExtended(newTabViewModel);
 
                _tabs.Add(newTab);
            }
        }

        public void SaveFile(TabItemExtended tab)
        {
            SaveFile(tab, true);
        }
        private bool SaveFile(TabItemExtended tab, bool isReturned)
        {
            if (tab == null)
                return false;

            if (tab.IsNewFile())
            {
                return SaveFileAs(tab, true);
            }

            tab.Save();
            return true;
        }

        public void SaveFileAs(TabItemExtended tab)
        {
            SaveFileAs(tab, true);
        }

        private bool SaveFileAs(TabItemExtended tab, bool isReturned)
        {
            if (tab == null)
                return false;

            SaveFileDialog saveFileDialog = new()
            {
                FileName = tab.TitleForSaveDialog(),
                Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                
                tab.Save(filePath);
                return true;
            }

            return false;
        }

        public void OpenSettings()
        {
            SettingsWindow settingsWindow = new()
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            settingsWindow.ShowDialog();
        }

        public void OpenClipboardHistory()
        {
            HistoryClipboardWindow historyWindow = new(_clipboardManager)
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            historyWindow.ShowDialog();
        }

        public void Copy(TabItemExtended tab)
        {
            tab.Copy(tab.RichTextBox.Selection.Text);
        }

        public void Cut(TabItemExtended tab)
        {
            tab.Copy(tab.RichTextBox.Selection.Text);
            tab.RichTextBox.Selection.Text = "";
        }

        public void Insert(TabItemExtended tab)
        {
            tab.RichTextBox.CaretPosition.InsertTextInRun(tab.GetTextFromBuffer());
        }

        public void PrintDocument(TabItemExtended tab)
        {

            if (SaveFile(tab, true))
            {
                // Создаем диалоговое окно печати
                PrintDialog printDialog = new();

                PrintWindow previewWindow = new(tab.Document())
                {
                    Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
                };
                previewWindow.ShowDialog();
            }
            
        }

        public void Undo(TabItemExtended tab)
        {

            tab.Undo();

        }

        public void Redo(TabItemExtended tab)
        {

            tab.Redo();

        }

        public bool isRedoAvailable(object parameter) => 
            parameter is TabItemExtended extended && extended.IsRedoAvailable();

        public bool isUndoAvailable(object parameter) =>
            parameter is TabItemExtended extended && extended.IsUndoAvailable();


        public void FindAndReplace(TabItemExtended tab)
        {
            var findWindow = new SearchWindow(tab)
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };

            findWindow.Show();
        }
    }
}
