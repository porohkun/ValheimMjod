using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ValheimCharacterTrainer
{
    public abstract class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!storage.Equals(value))
            {
                storage = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
