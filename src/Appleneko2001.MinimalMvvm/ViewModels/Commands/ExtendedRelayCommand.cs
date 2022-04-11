using System;
using MinimalMvvm.ViewModels.Commands.Interfaces;

namespace MinimalMvvm.ViewModels.Commands
{
    public class ExtendedRelayCommand : RelayCommand, IMayExecuteCommand
    {
        private Func<object, bool>? _mayExecute;
        
        // ReSharper disable once InconsistentNaming
        private event EventHandler? _mayExecuteChanged;

        public event EventHandler MayExecuteChanged
        {
            add => _mayExecuteChanged += value;
            remove => _mayExecuteChanged -= value;
        }

        public ExtendedRelayCommand(Action<object?> execute,
            Func<object?, bool>? canExecute = null,
            Func<object?, bool>? mayExecute = null) : base(execute, canExecute)
        {
            _mayExecute = mayExecute;
        }
        
        public bool MayExecute(object parameter)
        {
            var result = _mayExecute == null || _mayExecute(parameter);
            return result;
        }
        
        // Call this method to tell AvaloniaUI about this command can be executed at this moment.
        public void RaiseMayExecuteChanged()
        {
            _mayExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}