using System;
using System.Windows.Input;

namespace TicTacToe.Common
{
    public class RelayCommand : ICommand
    {
        private readonly Action _targetExecuteMethod;
        private readonly Func<bool> _targetCanExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }
        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        
        #region ICommand Members
        bool ICommand.CanExecute(object parameter)
        {
            return _targetCanExecuteMethod != null ? _targetCanExecuteMethod() : _targetExecuteMethod != null;
        }
        void ICommand.Execute(object parameter)
        {
            if (_targetExecuteMethod != null)
                _targetExecuteMethod();
        }

        public event EventHandler CanExecuteChanged = delegate { };
        #endregion
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _targetExecuteMethod;
        private readonly Func<T, bool> _targetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        
        #region ICommand Members
        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                var tparm = (T)parameter;
                return _targetCanExecuteMethod(tparm);
            }
            return _targetExecuteMethod != null;
        }
        void ICommand.Execute(object parameter)
        {
            if (_targetExecuteMethod != null)
                _targetExecuteMethod((T)parameter);
        }

        public event EventHandler CanExecuteChanged = delegate { };
        #endregion
    }
}