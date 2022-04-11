using System;
using System.Windows.Input;

namespace MinimalMvvm.ViewModels.Commands
{
    public class CommandBase : ViewModelBase, ICommand
    {
        private static event EventHandler<UnhandledExceptionEventArgs>? _onErrorOccur;
        
        public static event EventHandler<UnhandledExceptionEventArgs>? OnErrorHandler
        {
            add => _onErrorOccur += value;
            remove => _onErrorOccur -= value;
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
        
        protected void OnExecutionFailException(UnhandledExceptionEventArgs args)
        {
            _onErrorOccur?.Invoke(this, args);
        }
    }
}