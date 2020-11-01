using System;
using System.Windows.Input;

namespace AlertToCareFrontend.Commands
{
    public  class DelegateCommandClass : ICommand
    {
        readonly Action<object> _executeMethodAddress;
        readonly Func<object, bool> _canExecuteMethodAddress;

        public DelegateCommandClass(Action<object> executeMethodAddress,Func<object,bool> canExecuteMethodAddress)
        {
            _executeMethodAddress = executeMethodAddress;
            _canExecuteMethodAddress = canExecuteMethodAddress;
        }


        public event EventHandler CanExecuteChanged;

       

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethodAddress.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethodAddress.Invoke(parameter);
        }


        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
