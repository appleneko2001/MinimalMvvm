using System;
using System.Windows.Input;
using MinimalMvvm.Events;

namespace MinimalMvvm.ViewModels.Commands
{
    public class CommandBase : ViewModelBase, ICommand
    {
        private static event EventHandler<ExecutionFailExceptionArgs>? _onErrorEvent;
        
        public static event EventHandler<ExecutionFailExceptionArgs>? OnErrorHandler
        {
            add => _onErrorEvent += value;
            remove => _onErrorEvent -= value;
        }

        public event EventHandler? CanExecuteChanged;
        
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public virtual void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        
        protected void OnExecutionFailException(ExecutionFailExceptionArgs args)
        {
            _onErrorEvent?.Invoke(this, args);
        }
    }
}