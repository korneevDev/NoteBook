using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteBookUI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((App)Application.Current).ChangeCulture();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CommandHandlers.MainComandHandler handler)
            {
                // Выполняем команду NewTabCommand
                handler.FileCommands.NewTabCommand.Execute(null);
            }
        }
    }
}