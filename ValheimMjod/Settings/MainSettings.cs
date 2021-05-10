using Newtonsoft.Json;

namespace ValheimMjod
{
    public class MainSettings : SettingsPartWithNotifier
    {
        [JsonProperty(nameof(FirstLaunch))]
        private bool _firstLaunch = true;

        [JsonProperty(nameof(UserId))]
        private string _userId = null;

        [JsonIgnore]
        public bool FirstLaunch
        {
            get => _firstLaunch;
            set => SetProperty(ref _firstLaunch, value);
        }

        [JsonIgnore]
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
    }
}
