using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class FileCommandsHandler(MainViewModel mainViewModel)
    {
        private readonly MainViewModel mainViewModel = mainViewModel;
        public ObservableCollection<FileView> Tabs => mainViewModel.GetTabsList();
        public ICommand CloseTabCommand { get; } = new RelayCommand<FileView>(mainViewModel.CloseTab);

        public ICommand NewTabCommand { get; } = new RelayCommand(mainViewModel.CreateNewTab);
        public ICommand OpenFileCommand { get; } = new RelayCommand(mainViewModel.OpenExisttingFile);

        public ICommand SaveFileCommand { get; } = new RelayCommand<FileView>(mainViewModel.SaveFile, mainViewModel.CanExecuteFileCommand);

        public ICommand SaveFileAsCommand { get; } = new RelayCommand<FileView>(mainViewModel.SaveFileAs, mainViewModel.CanExecuteFileCommand);
    }
}
