using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using NoteBookLib;
using NoteBookUI.Utils;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteBookUI.View
{
    public class MainViewModel : OnPropertyChangedHandler
    {

        private readonly ObservableCollection<TabItemExtended> _tabs;
        private readonly ClipboardManager _clipboardManager;
        private readonly IPathFormatter _pathFormatter;
        private FontFamily _selectedFont;
        private double _selectedFontSize;

        private readonly ObservableCollection<FontFamily> _fonts;
        private readonly ObservableCollection<double> _fontSizes;



        public MainViewModel()
        {
            _tabs = [];
            _clipboardManager = new ClipboardManager();
            _pathFormatter = new IPathFormatter.Base();
            App.LanguageChanged += UpdateTabTitles;

            // Инициализация доступных шрифтов и размеров
            _fonts = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies);
            _fontSizes = new ObservableCollection<double>(new[] { 8.0, 10.0, 12.0, 14.0, 16.0, 18.0, 20.0, 24.0 });
            _selectedFont = _fonts.FirstOrDefault(); // Устанавливаем начальный шрифт
            _selectedFontSize = _fontSizes.FirstOrDefault(); // Устанавливаем начальный размер

        }

        private void UpdateTabTitles()
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateTitle();
            }
        }

        public ObservableCollection<TabItemExtended> GetTabsList() => _tabs;

        public async void CreateNewTab()
        {
            var tabTextEditor = new TextEditor(_clipboardManager);
            await tabTextEditor.CreateFile();
            var newTab = new TabItemExtended(tabTextEditor, _selectedFont, _selectedFontSize);
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

        public async void OpenExisttingFile()
        {
            // Создаем диалог открытия файла
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {

                var newTabViewModel = new TextEditor(_clipboardManager);

                await newTabViewModel.LoadFile(openFileDialog.FileName);

                var newTab = new TabItemExtended(newTabViewModel, _selectedFont, _selectedFontSize);
 
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
                FileName = _pathFormatter.Format(tab.TitleForSaveDialog() + ".txt"),
                Filter = tab.GetOpenFileTemplate(),
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
            tab.Copy(tab.TextBox.SelectedText);
        }

        public void Cut(TabItemExtended tab)
        {
            tab.Copy(tab.TextBox.SelectedText);
            tab.TextBox.SelectedText = "";
        }

        public void Insert(TabItemExtended tab)
        {
            int caretIndex = tab.TextBox.CaretIndex;
            tab.TextBox.Text = tab.TextBox.Text.Insert(caretIndex, tab.GetTextFromBuffer());
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

        public ObservableCollection<FontFamily> GetAvailableFonts() => _fonts;

        public ObservableCollection<double> GetAvailableFontSizes() => _fontSizes;

        public FontFamily GetSelectedFont() => _selectedFont;

        public double GetSelectedFontSize() => _selectedFontSize;

        public void SetSelectedFont(FontFamily selectedFont)
        {
            _selectedFont = selectedFont;
            foreach (var tab in _tabs)
            {
                tab.UpdateFont(selectedFont);
            }
        }

        public void SetSelectedFontSize(double selectedFontSize)
        {
            _selectedFontSize = selectedFontSize;
            foreach (var tab in _tabs)
            {
                tab.UpdateFontSize(selectedFontSize);
            }
        }
    }
}
