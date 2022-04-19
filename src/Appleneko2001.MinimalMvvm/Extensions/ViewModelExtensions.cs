using System.Runtime.CompilerServices;
using MinimalMvvm.ViewModels;

namespace MinimalMvvm.Extensions
{
    public static class ViewModelExtensions
    {
        public static void SetAndUpdateIfChanged<T>(this ViewModelBase vm, ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            vm.RaisePropertyChangedInternal(propertyName);
        }
    }
}