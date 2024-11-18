using NoteBookUI.View;
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
