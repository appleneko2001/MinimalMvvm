using System.ComponentModel;
using System.Runtime.CompilerServices;
using MinimalMvvm.ViewModels.Messengers;
using MinimalMvvm.ViewModels.Messengers.Interfaces;

namespace MinimalMvvm.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private static object _messengerCreationLock = new object();
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
                
                lock (_messengerCreationLock)
                    return _messenger = new Messenger();
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}