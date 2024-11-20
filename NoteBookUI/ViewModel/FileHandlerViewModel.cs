using Microsoft.Win32;
using NoteBookLib;
using NoteBookUI.Utils;
using NoteBookUI.View;
using System.Windows;

namespace NoteBookUI.ViewModel
{
    public class FileHandlerViewModel
    {
        private readonly ClipboardManager _clipboardManager;
        private readonly IPathFormatter _pathFormatter;
        private readonly FontViewModel _fontViewModel;
        private readonly TabsViewModel _tabsViewModel;

        public FileHandlerViewModel(
            FontViewModel fontViewModel, 
            ClipboardManager clipboardManager,
            IPathFormatter pathFormatter,
            TabsViewModel tabsViewModel
            )
        {
            _clipboardManager = clipboardManager;
            _pathFormatter = pathFormatter;
            _fontViewModel = fontViewModel;
            _tabsViewModel = tabsViewModel;
            _tabsViewModel = tabsViewModel;
        }



        public async void CreateNewTab()
        {

            var tabTextEditor = new TextEditor(_clipboardManager);
            var interval = Settings.Default.AutoSaveInterval ?? "No";
            await tabTextEditor.CreateFile(interval);
            var newTab = _fontViewModel.CreateNewTab(tabTextEditor);

            _tabsViewModel.AddTab(newTab);
        }

        public void CloseTab(FileView tab)
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

            _tabsViewModel.RemoveTab(tab);

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

                var interval = Settings.Default.AutoSaveInterval ?? "No";

                await newTabViewModel.LoadFile(openFileDialog.FileName, interval);

                var newTab = _fontViewModel.CreateNewTab(newTabViewModel);

                _tabsViewModel.AddTab(newTab);
            }
        }

        public void SaveFile(FileView tab)
        {
            SaveBool(tab);
        }
        public bool SaveBool(FileView tab)
        {
            if (tab == null)
                return false;

            if (tab.IsNewFile())
                return SaveAs(tab);

            tab.Save();
            return true;
        }

        public void SaveFileAs(FileView tab)
        {
            SaveAs(tab);
        }

        private bool SaveAs(FileView tab)
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

    }
}
