using System;
using System.Windows.Input;

namespace MinimalMvvm.ViewModels.Commands
{
    public class CommandBase : ViewModelBase, ICommand
    {
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
    }
}