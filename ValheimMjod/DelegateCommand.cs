using System;
using System.Windows.Input;

namespace ValheimMjod
{
    public class DelegateCommand : ICommand
    {
        private Action _executeMethod;
        private Func<bool> _canExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : this(executeMethod)
        {
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke();
        }

    }
}
