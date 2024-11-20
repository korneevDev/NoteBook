using NoteBookUI.View;
using NoteBookUI.ViewModel;
using System.Windows.Input;

namespace NoteBookUI.CommandHandlers
{
    public class EditCommandsHandler(
        EditFileViewModel _editViewModel, 
        TabsViewModel _tabsViewModel
        )
    {
        public ICommand CopyCommand { get; } = new RelayCommand<FileView>(_editViewModel.Copy, _tabsViewModel.CanExecuteTabCommand);
        public ICommand InsertCommand { get; } = new RelayCommand<FileView>(_editViewModel.Insert, _editViewModel.IsInsertAvailable);
        public ICommand CutCommand { get; } = new RelayCommand<FileView>(_editViewModel.Cut, _tabsViewModel.CanExecuteTabCommand);

        public ICommand FindCommand { get; } = new RelayCommand<FileView>(_editViewModel.FindAndReplace, _tabsViewModel.CanExecuteTabCommand);

        public ICommand UndoCommand { get; } = new RelayCommand<FileView>(_editViewModel.Undo, _editViewModel.IsUndoAvailable);

        public ICommand RedoCommand { get; } = new RelayCommand<FileView>(_editViewModel.Redo, _editViewModel.IsRedoAvailable);
    }
}
