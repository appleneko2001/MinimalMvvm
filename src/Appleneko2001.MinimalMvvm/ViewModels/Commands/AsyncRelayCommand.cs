using System;
using System.Threading.Tasks;
using MinimalMvvm.Events;

namespace MinimalMvvm.ViewModels.Commands
{
    public class AsyncRelayCommand : CommandBase
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            protected set
            {
                _isRunning = value;
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public AsyncRelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
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
            if (IsRunning)
                return;

            Task.Factory.StartNew(delegate
            {
                IsRunning = true;
                _execute(parameter);
            }).ContinueWith(delegate(Task task)
            {
                // Catch any exceptions that happened on a background thread
                if (task.Exception != null)
                {
                    var e = task.Exception;
                    
                    var args = new ExecutionFailExceptionArgs(e);
                    OnExecutionFailException(args);

                    if (!args.Handled)
                        throw e;
                }
                
                IsRunning = false;
            });
        }
    }
}