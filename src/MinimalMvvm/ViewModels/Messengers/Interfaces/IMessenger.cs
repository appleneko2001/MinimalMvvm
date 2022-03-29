using System;

namespace MinimalMvvm.ViewModels.Messengers.Interfaces
{
    public interface IMessenger
    {
        void Subscribe<TMessage>(Action<TMessage> action);
        
        void Unsubscribe<TMessage>(Action<TMessage> action);
        
        void Publish<TMessage>(TMessage message);
    }
}