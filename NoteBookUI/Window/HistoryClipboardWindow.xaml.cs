using NoteBookLib.FeatureManager;
using System.Windows;
using System.Windows.Controls;

namespace NoteBookUI
{

    public partial class HistoryClipboardWindow : Window
    {
        private readonly ClipboardManager _clipboardManager;

        public HistoryClipboardWindow(ClipboardManager clipboardManager)
        {
            InitializeComponent();

            _clipboardManager = clipboardManager;
            historyItemsControl.ItemsSource = _clipboardManager.GetBuffer();
        }

        private void CopyFromHistory_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string textToCopy = (string)button.DataContext; // Извлекаем текст элемента
                int index = historyItemsControl.Items.IndexOf(textToCopy);

                _clipboardManager.SetOldValueToBufferTop(index);

                Close();
            }
        }
    }
}
