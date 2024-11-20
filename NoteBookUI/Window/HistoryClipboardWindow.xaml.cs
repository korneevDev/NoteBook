using NoteBookUI.View;
using System.Windows;
using System.Windows.Controls;

namespace NoteBookUI
{

    public partial class HistoryClipboardWindow : Window
    {
        private readonly FileView _view;

        public HistoryClipboardWindow(FileView fileView)
        {
            InitializeComponent();

            _view = fileView;
            historyItemsControl.ItemsSource = _view.GetBuffer();
        }

        private void CopyFromHistory_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string textToCopy = (string)button.DataContext; // Извлекаем текст элемента
                int index = historyItemsControl.Items.IndexOf(textToCopy);

                _view.SetOldValueToBufferTop(index);

                Close();
            }
        }
    }
}
