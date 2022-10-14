using System;

namespace MinimalMvvm.ViewModels.Commands
{
    public class RelayCommand : TryCommandBase
    {
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

        protected Action<object?> OnExecuteInternal => _execute;
        protected Func<object?, bool>? CanExecuteInternal => _canExecute;

        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        private bool _isRunning;


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
            OnExecuteProcedure(parameter);
            IsRunning = false;
        }

        protected override void OnTryExecute(object? p)
        {
            _execute(p);
        }
    }
}