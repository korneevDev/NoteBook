using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class ViewCommandsHandler(MainViewModel mainViewModel)
    {
        public ICommand OpenSettingsCommand { get; } = new RelayCommand(mainViewModel.OpenSettings);
        public ICommand OpenHistoryCommand { get; } = new RelayCommand(mainViewModel.OpenClipboardHistory);
        public ICommand PrintCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.PrintDocument, mainViewModel.CanExecuteFileCommand);
    }
}
