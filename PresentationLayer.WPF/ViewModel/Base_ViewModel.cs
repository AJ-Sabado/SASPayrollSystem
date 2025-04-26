using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationLayer.WPF.ViewModel
{
    public class Base_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}
