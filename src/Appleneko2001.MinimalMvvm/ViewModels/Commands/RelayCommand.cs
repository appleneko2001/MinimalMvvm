using System;

namespace MinimalMvvm.ViewModels.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

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
            
            return _canExecute?.Invoke() ?? true;
        }

        public override void Execute(object? parameter)
        {
            IsRunning = true;
            try
            {
                _execute();
            }
            catch (Exception e)
            {
                var args = new UnhandledExceptionEventArgs(e, false);
                OnExecutionFailException(args);

                if (args.IsTerminating)
                    throw;
            }

            IsRunning = false;
        }
    }
}