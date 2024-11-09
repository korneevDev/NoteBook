using System.Collections.ObjectModel;
using System.Windows.Input;


namespace NoteBookUI.View
{
    public class MainViewComandManager
    {

        private readonly MainViewModel mainViewModel;
        public ObservableCollection<TabItemExtended> Tabs => mainViewModel.GetTabsList();
        

        public ICommand NewTabCommand { get; }
        public ICommand CloseTabCommand { get; }

        public ICommand OpenFileCommand { get; }

        public ICommand SaveFileCommand { get; }

        public ICommand SaveFileAsCommand { get; }

        public ICommand OpenSettings {  get; }

        public MainViewComandManager()
        {
            mainViewModel = new MainViewModel();

            NewTabCommand = new RelayCommand(mainViewModel.CreateNewTab);
            CloseTabCommand = new RelayCommand<TabItemExtended>(mainViewModel.CloseTab);
            OpenFileCommand = new RelayCommand(mainViewModel.OpenExisttingFile);
            SaveFileCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFile, CanExecuteFileCommand);
            SaveFileAsCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFileAs, CanExecuteFileCommand);
            OpenSettings = new RelayCommand(mainViewModel.OpenSettings);

        }

        private bool CanExecuteFileCommand(object parameter) => parameter is TabItemExtended;
    }
}
