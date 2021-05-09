using Newtonsoft.Json;

namespace ValheimMjod
{
    public class MainSettings : SettingsPartWithNotifier
    {
        [JsonProperty(nameof(FirstLaunch))]
        private bool _firstLaunch = true;

        [JsonIgnore]
        public bool FirstLaunch
        {
            get => _firstLaunch;
            set => SetProperty(ref _firstLaunch, value);
        }
    }
}
