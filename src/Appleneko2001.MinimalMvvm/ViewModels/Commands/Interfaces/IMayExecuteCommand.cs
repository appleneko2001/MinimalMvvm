using System;

namespace MinimalMvvm.ViewModels.Commands.Interfaces
{
    public interface IMayExecuteCommand
    {
        event EventHandler MayExecuteChanged;

        bool MayExecute(object parameter);
    }
}