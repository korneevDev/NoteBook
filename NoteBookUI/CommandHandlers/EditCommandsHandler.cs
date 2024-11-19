using NoteBookUI.View;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class EditCommandsHandler(MainViewModel mainViewModel)
    {
        public ICommand CopyCommand { get; } = new RelayCommand<FileView>(mainViewModel.Copy, mainViewModel.CanExecuteFileCommand);
        public ICommand InsertCommand { get; } = new RelayCommand<FileView>(mainViewModel.Insert, mainViewModel.CanExecuteFileCommand);
        public ICommand CutCommand { get; } = new RelayCommand<FileView>(mainViewModel.Cut, mainViewModel.CanExecuteFileCommand);

        public ICommand FindCommand { get; } = new RelayCommand<FileView>(mainViewModel.FindAndReplace, mainViewModel.CanExecuteFileCommand);

        public ICommand UndoCommand { get; } = new RelayCommand<FileView>(mainViewModel.Undo, mainViewModel.isUndoAvailable);

        public ICommand RedoCommand { get; } = new RelayCommand<FileView>(mainViewModel.Redo, mainViewModel.isRedoAvailable);
    }
}
