using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MinimalMvvm.ViewModels.Messengers;
using MinimalMvvm.ViewModels.Messengers.Interfaces;

namespace MinimalMvvm.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private static readonly object MessengerCreationLock = new ();
        private static IMessenger? _messenger;
        
        public static void SetMessenger(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public IMessenger Messenger
        {
            get
            {
                if (_messenger != null)
                    return _messenger;
                
                lock (MessengerCreationLock)
                    return _messenger = new Messenger();
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        /// <summary>
        /// This event used for Reactive patterns.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs>? RxPropertyChanged; 

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
            RxPropertyChanged?.Invoke(this, args);
        }

        internal void RaisePropertyChangedInternal([CallerMemberName] string? propertyName = null) =>
            OnPropertyChanged(propertyName);
    }
}