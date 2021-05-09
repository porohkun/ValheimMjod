using Newtonsoft.Json;
using System.Windows;

namespace ValheimMjod
{
    public class WindowStateSettings : SettingsPartWithNotifier
    {
        [JsonProperty(nameof(Width))]
        private double _width;

        [JsonProperty(nameof(Height))]
        private double _height;

        [JsonProperty(nameof(State))]
        private WindowState _state;

        [JsonIgnore]
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        [JsonIgnore]
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        [JsonIgnore]
        public WindowState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public WindowStateSettings() : this(640, 480, WindowState.Normal) { }

        public WindowStateSettings(int width, int height, WindowState state)
        {
            _width = width;
            _height = height;
            _state = state;
        }
    }
}
