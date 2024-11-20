using NoteBookUI.View;
using NoteBookUI.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class ViewCommandsHandler(
        OpenWindowDialogViewModel mainViewModel, 
        TabsViewModel tabsViewModel
        )
    {
        public ICommand OpenSettingsCommand { get; } = new RelayCommand(mainViewModel.OpenSettings);
        public ICommand OpenHistoryCommand { get; } = new RelayCommand(mainViewModel.OpenClipboardHistory);
        public ICommand PrintCommand { get; } = new RelayCommand<FileView>(mainViewModel.PrintDocument, tabsViewModel.CanExecuteTabCommand);
    }
}
