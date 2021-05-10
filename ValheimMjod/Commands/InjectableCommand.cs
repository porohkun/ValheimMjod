using System;
using System.Windows.Input;

namespace ValheimMjod.Commands
{
    public static class InjectableCommand
    {
        internal static TParam CastParam<TParam>(object parameter)
        {
            var typeofTParam = typeof(TParam);
            if (typeofTParam == typeof(bool))
                return (TParam)(object)(parameter == null ? false : (parameter is bool ? (bool)parameter : false));

            if (parameter == null)
                if (!typeofTParam.IsCanBeNull())
                    throw new ArgumentException($"Type '{nameof(TParam)}' can't be null");
                else
                    return (TParam)(object)null;
            else if (parameter is TParam param)
                return param;
            else
                throw new ArgumentException($"Cant cast type '{parameter.GetType().Name}' into '{nameof(TParam)}'");
        }
    }

    public abstract class InjectableCommand<T> : ICommand where T : InjectableCommand<T>
    {

        public void Execute(object parameter)
        {
            ExecuteInternal(parameter);
        }
        protected abstract void ExecuteInternal(object parameter);

        public bool CanExecute(object parameter)
        {
            return CanExecuteInternal(parameter);
        }
        protected abstract bool CanExecuteInternal(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public abstract class InjectableCommand<T, TParam> : ICommand where T : InjectableCommand<T, TParam>
    {

        public void Execute(object parameter)
        {
            Execute(InjectableCommand.CastParam<TParam>(parameter));
        }
        public void Execute(TParam parameter)
        {
            ExecuteInternal(parameter);
        }
        protected abstract void ExecuteInternal(TParam parameter);

        public bool CanExecute(object parameter)
        {
            try
            {
                return CanExecute(InjectableCommand.CastParam<TParam>(parameter));
            }
            catch
            {
                return false;
            }
        }

        public bool CanExecute(TParam parameter)
        {
            return CanExecuteInternal(parameter);
        }
        protected abstract bool CanExecuteInternal(TParam parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
