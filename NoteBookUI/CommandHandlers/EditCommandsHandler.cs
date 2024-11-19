using NoteBookUI.View;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class EditCommandsHandler(MainViewModel mainViewModel)
    {
        public ICommand CopyCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.Copy, mainViewModel.CanExecuteFileCommand);
        public ICommand InsertCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.Insert, mainViewModel.CanExecuteFileCommand);
        public ICommand CutCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.Cut, mainViewModel.CanExecuteFileCommand);

        public ICommand FindCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.FindAndReplace, mainViewModel.CanExecuteFileCommand);

        public ICommand UndoCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.Undo, mainViewModel.isUndoAvailable);

        public ICommand RedoCommand { get; } = new RelayCommand<TabItemExtended>(mainViewModel.Redo, mainViewModel.isRedoAvailable);
    }
}
