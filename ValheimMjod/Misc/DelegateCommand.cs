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

    public class DelegateCommand<T> : ICommand
    {
        private Action<T> _executeMethod;
        private Func<T, bool> _canExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<T> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : this(executeMethod)
        {
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod?.Invoke(CastToT(parameter)) ?? true;
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke(CastToT(parameter));
        }

        private static T CastToT(object parameter)
        {
            T param = default(T);
            if (parameter is T tparam)
                param = tparam;
            return param;
        }
    }
}
