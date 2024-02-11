using System.Windows.Input;

namespace Lexington.Command
{
    internal class RelayCommand <T> : ICommand
    {
        //private readonly Action _Execute;
        //private readonly Func<bool> _CanExecute;

        //public RelayCommand(Action execute, Func<bool> canExecute = null)
        //{
        //    _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        //    _CanExecute = canExecute;
        //}


        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        //public bool CanExecute(object parameter)
        //{
        //    return _CanExecute == null || _CanExecute();
        //}

        //public void Execute(object parameter)
        //{
        //    _Execute();
        //}

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

}
