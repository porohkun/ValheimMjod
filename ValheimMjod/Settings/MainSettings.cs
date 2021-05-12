using Newtonsoft.Json;
using System;

namespace ValheimMjod
{
    public class MainSettings : SettingsPartWithNotifier
    {
        [JsonProperty(nameof(LastLaunchedVersion))]
        private Version _lastLaunchedVersion ;

        [JsonProperty(nameof(UserId))]
        private string _userId = null;

        [JsonIgnore]
        public Version LastLaunchedVersion
        {
            get => _lastLaunchedVersion;
            set => SetProperty(ref _lastLaunchedVersion, value);
        }

        [JsonIgnore]
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
    }
}
