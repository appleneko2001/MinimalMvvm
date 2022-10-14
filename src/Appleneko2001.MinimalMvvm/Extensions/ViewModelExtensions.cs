using System.Runtime.CompilerServices;
using MinimalMvvm.ViewModels;

namespace MinimalMvvm.Extensions
{
    public static class ViewModelExtensions
    {
        public static bool SetAndUpdateIfChanged<T>(this ViewModelBase vm, ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            vm.RaisePropertyChangedInternal(propertyName);
            return true;
        }
    }
}