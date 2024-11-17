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

        public ICommand CopyCommand { get; }
        public ICommand InsertCommand { get; }
        public ICommand CutCommand { get; }

        public ICommand OpenHistoryCommand { get; }
        public ICommand PrintCommand { get; }

        public ICommand UndoCommand { get; }

        public ICommand RedoCommand { get; }

        public MainViewComandManager()
        {
            mainViewModel = new MainViewModel();

            NewTabCommand = new RelayCommand(mainViewModel.CreateNewTab);
            CloseTabCommand = new RelayCommand<TabItemExtended>(mainViewModel.CloseTab);
            OpenFileCommand = new RelayCommand(mainViewModel.OpenExisttingFile);
            SaveFileCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFile, CanExecuteFileCommand);
            SaveFileAsCommand = new RelayCommand<TabItemExtended>(mainViewModel.SaveFileAs, CanExecuteFileCommand);
            OpenSettings = new RelayCommand(mainViewModel.OpenSettings);

            CopyCommand = new RelayCommand<TabItemExtended>(mainViewModel.Copy, CanExecuteFileCommand);
            CutCommand = new RelayCommand<TabItemExtended>(mainViewModel.Cut, CanExecuteFileCommand);
            InsertCommand = new RelayCommand<TabItemExtended>(mainViewModel.Insert, CanExecuteFileCommand);

            OpenHistoryCommand = new RelayCommand(mainViewModel.OpenClipboardHistory);

            PrintCommand = new RelayCommand<TabItemExtended>(mainViewModel.PrintDocument, CanExecuteFileCommand);

            UndoCommand = new RelayCommand<TabItemExtended>(mainViewModel.Undo, mainViewModel.isUndoAvailable);
            RedoCommand = new RelayCommand<TabItemExtended>(mainViewModel.Redo, mainViewModel.isRedoAvailable);
        }

        private bool CanExecuteFileCommand(object parameter) => parameter is TabItemExtended;
    }
}
