using System.Windows.Input;

namespace NoteBookUI
{

    public class RelayCommand(
        Action execute, 
        Func<bool>? canExecute = null
        ) : ICommand
    {
        private readonly Action _execute = 
            execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<bool>? _canExecute = canExecute;

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand<T>(
        Action<T> execute, 
        Func<T, bool>? canExecute = null
        ) : ICommand
    {
        private readonly Action<T> _execute = 
            execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<T, bool>? _canExecute = canExecute;

        public bool CanExecute(object? parameter) =>
            _canExecute == null || parameter == null ||_canExecute((T)parameter);
        

        public void Execute(object? parameter)
        {
            if (parameter != null)
                _execute((T)parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}