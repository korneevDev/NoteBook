using System.Collections.ObjectModel;
using System.Windows.Input;


namespace NoteBookUI.View
{
    public class MainViewComandManager
    {
        public ObservableCollection<TabItemExtended> Tabs => mainViewModel.Tabs;
        

        public ICommand NewTabCommand { get; }
        public ICommand CloseTabCommand { get; }

        public ICommand OpenFileCommand { get; }

        private MainViewModel mainViewModel;


        public MainViewComandManager()
        {
            mainViewModel = new MainViewModel();

            NewTabCommand = new RelayCommand(mainViewModel.CreateNewTab);
            CloseTabCommand = new RelayCommand<TabItemExtended>(mainViewModel.CloseTab);
            OpenFileCommand = new RelayCommand(mainViewModel.OpenExisttingFile);
        }


    }
}
