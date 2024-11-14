using System.Windows;

namespace NoteBookUI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((App)Application.Current).ChangeCulture();
        }
    }
}