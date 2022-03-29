using System;
using System.Collections.Generic;
using MinimalMvvm.ViewModels.Messengers.Interfaces;

namespace MinimalMvvm.ViewModels.Messengers
{
    public class Messenger : IMessenger
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers.Add(messageType, new List<Delegate>());
            }

            _subscribers[messageType].Add(action);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (!_subscribers.ContainsKey(messageType))
            {
                return;
            }

            _subscribers[messageType].Remove(action);
        }

        public void Publish<TMessage>(TMessage message)
        {
            var messageType = typeof(TMessage);
            if (!_subscribers.ContainsKey(messageType))
            {
                return;
            }

            foreach (var action in _subscribers[messageType])
            {
                ((Action<TMessage>)action)(message);
            }
        }
    }
}