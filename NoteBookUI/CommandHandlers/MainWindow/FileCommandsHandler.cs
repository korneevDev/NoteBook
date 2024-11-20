using NoteBookUI.View;
using NoteBookUI.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class FileCommandsHandler(
        FileHandlerViewModel _mainViewModel, 
        TabsViewModel _tabsViewModel
        )
    {
        public ObservableCollection<FileView> Tabs => _tabsViewModel.GetTabsList();
        public ICommand CloseTabCommand { get; } = new RelayCommand<FileView>(_mainViewModel.CloseTab);

        public ICommand NewTabCommand { get; } = new RelayCommand(_mainViewModel.CreateNewTab);
        public ICommand OpenFileCommand { get; } = new RelayCommand(_mainViewModel.OpenExisttingFile);

        public ICommand SaveFileCommand { get; } = new RelayCommand<FileView>(_mainViewModel.SaveFile, _tabsViewModel.CanExecuteTabCommand);

        public ICommand SaveFileAsCommand { get; } = new RelayCommand<FileView>(_mainViewModel.SaveFileAs, _tabsViewModel.CanExecuteTabCommand);
    }
}
