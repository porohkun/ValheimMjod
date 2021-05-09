using System.ComponentModel;

namespace ValheimMjod
{
    public interface ISettingsPartWithNotifier
    {
        bool NotifierSubscribed { get; }
        void SubscribeNotifier(PropertyChangedEventHandler handler);
        void UnsubscribeNotifier(PropertyChangedEventHandler handler);
    }

    public abstract class SettingsPartWithNotifier : BindableBase, ISettingsPartWithNotifier
    {
        bool _notifierSubscribed;
        bool ISettingsPartWithNotifier.NotifierSubscribed => _notifierSubscribed;

        void ISettingsPartWithNotifier.SubscribeNotifier(PropertyChangedEventHandler handler)
        {
            PropertyChanged += handler;
            _notifierSubscribed = true;
        }

        void ISettingsPartWithNotifier.UnsubscribeNotifier(PropertyChangedEventHandler handler)
        {
            PropertyChanged -= handler;
            _notifierSubscribed = false;
        }
    }
}
