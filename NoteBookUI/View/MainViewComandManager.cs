using System.Collections.ObjectModel;
using System.Windows.Input;


namespace NoteBookUI.View
{
    public class MainViewComandManager
    {

        private readonly MainViewModel mainViewModel;
        public ObservableCollection<TabItemExtended> Tabs => mainViewModel.getTabsList();
        

        public ICommand NewTabCommand { get; }
        public ICommand CloseTabCommand { get; }

        public ICommand OpenFileCommand { get; }

        public ICommand SaveFileCommand { get; }

        public ICommand SaveFileAsCommand { get; }

        public MainViewComandManager()
        {
            mainViewModel = new MainViewModel();

            NewTabCommand = new RelayCommand(mainViewModel.CreateNewTab);
            CloseTabCommand = new RelayCommand<TabItemExtended>(mainViewModel.CloseTab);
            OpenFileCommand = new RelayCommand(mainViewModel.OpenExisttingFile);
            SaveFileCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFile, CanExecuteFileCommand);
            SaveFileAsCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFileAs, CanExecuteFileCommand);
        }

        private bool CanExecuteFileCommand(object parameter) => parameter is TabItemExtended;
    }
}
