using NoteBookUI.View;
using System.Windows;

namespace NoteBookUI
{
    public partial class SearchWindow : Window
    {
        private readonly FileView _tab;
        public SearchWindow(FileView tab)
        {
            InitializeComponent();
            _tab = tab;
        }

        private void FindNextButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(SearchTextBox.Text))
                _tab.FindNextString(SearchTextBox.Text);
        }

        private void ReplaceAllButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(SearchTextBox.Text) && !string.IsNullOrEmpty(SearchTextBox.Text))
                _tab.ReplaceString(SearchTextBox.Text, ReplaceTextBox.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text) && !string.IsNullOrEmpty(SearchTextBox.Text))
                _tab.FindAndReplaceString(SearchTextBox.Text, ReplaceTextBox.Text);
        }
    }
}
