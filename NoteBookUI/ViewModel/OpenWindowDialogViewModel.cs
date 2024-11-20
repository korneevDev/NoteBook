using NoteBookUI.View;
using System.Windows;
using System.Windows.Controls;

namespace NoteBookUI.ViewModel
{
    public class OpenWindowDialogViewModel(
        FileHandlerViewModel fileHandler, 
        FontViewModel fontViewModel
        )
    {

        public void PrintDocument(FileView tab)
        {
            if (tab == null || !fileHandler.SaveBool(tab))
                return;

            // Создаем диалоговое окно печати
            PrintDialog printDialog = new();

            PrintWindow previewWindow = fontViewModel.CreatePrintWindow(tab) ;

            previewWindow.Owner = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);
            previewWindow.ShowDialog();
        }

        public void OpenSettings()
        {
            SettingsWindow settingsWindow = new()
            {
                Owner = Application.Current.Windows
                    .OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            settingsWindow.ShowDialog();
        }

        public void OpenClipboardHistory(FileView tab)
        {
            HistoryClipboardWindow historyWindow = new(tab)
            {
                Owner = Application.Current.Windows
                    .OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            historyWindow.ShowDialog();
        }
    }
}
