using System;
using MinimalMvvm.Events;

namespace MinimalMvvm.ViewModels.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                _isRunning = value;
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            if (_isRunning)
                return false;
            
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object? parameter)
        {
            IsRunning = true;
            try
            {
                _execute(parameter);
            }
            catch (Exception e)
            {
                var args = new ExecutionFailExceptionArgs(e);
                OnExecutionFailException(args);

                if (!args.Handled)
                    throw;
            }

            IsRunning = false;
        }
    }
}