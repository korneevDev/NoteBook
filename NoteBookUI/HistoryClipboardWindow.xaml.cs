using NoteBookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HistoryClipboardWindow : Window
    {
        private readonly ClipboardManager _clipboardManager;

        public HistoryClipboardWindow(ClipboardManager clipboardManager)
        {
            InitializeComponent();

            _clipboardManager = clipboardManager;
            historyItemsControl.ItemsSource = _clipboardManager.GetBuffer();
        }

        private void CopyFromHistory_Click(object sender, RoutedEventArgs e)
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
