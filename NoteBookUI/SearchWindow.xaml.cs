using NoteBookUI.View;
using System.Windows;

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly TabItemExtended _tab;
        public SearchWindow(TabItemExtended tab)
        {
            InitializeComponent();
            _tab = tab;
        }

        private void FindNextButton_Click(object sender, RoutedEventArgs e)
        {
            _tab.FindNextString(SearchTextBox.Text);
        }

        private void ReplaceAllButton_Click(object sender, RoutedEventArgs e)
        {
            _tab.ReplaceString(SearchTextBox.Text, ReplaceTextBox.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            _tab.FindAndReplaceString(SearchTextBox.Text, ReplaceTextBox.Text);
        }
    }
}
