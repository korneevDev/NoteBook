using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class FileCommandsHandler(MainViewModel mainViewModel)
    {
        private readonly MainViewModel mainViewModel = mainViewModel;
        public ObservableCollection<TabItemExtended> Tabs => mainViewModel.GetTabsList();
        public ICommand CloseTabCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.CloseTab);

        public ICommand NewTabCommand { get; } = new RelayCommand(mainViewModel.CreateNewTab);
        public ICommand OpenFileCommand { get; } = new RelayCommand(mainViewModel.OpenExisttingFile);

        public ICommand SaveFileCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.SaveFile, mainViewModel.CanExecuteFileCommand);

        public ICommand SaveFileAsCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.SaveFileAs, mainViewModel.CanExecuteFileCommand);
    }
}
