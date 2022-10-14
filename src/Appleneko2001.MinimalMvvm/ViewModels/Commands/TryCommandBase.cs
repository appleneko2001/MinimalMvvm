using System;

namespace MinimalMvvm.ViewModels.Commands
{
    public class TryCommandBase : CommandBase
    {
        public override void Execute(object? parameter)
        {
            OnExecuteProcedure(parameter);
        }

        protected virtual void OnExecuteProcedure(object? p)
        {
            try
            {
                OnTryExecute(p);
            }
            catch (Exception e)
            {
                var handled = DispatchErrorEventInternal(this, e);
                if (!handled)
                    throw;
            }
        }

        protected virtual void OnTryExecute(object? p)
        {
            throw new NotImplementedException();
        }
    }
}