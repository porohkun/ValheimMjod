using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;

namespace ValheimMjod
{
    [Serializable]
    public class Settings
    {
        public static Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static string UpdatePath => @"https://github.com/porohkun/ValheimMjod/releases/latest/download/";
        public static string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ValheimMjod");
        public static string SettingsPath => Path.Combine(AppDataPath, "settings.json");
        public static string AppPath => AppDomain.CurrentDomain.BaseDirectory;

        public static Settings Instance { get; private set; }

        private static JsonSerializer _serializer;

        private event Action PropertyChanged;

        static Settings()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                return;
            Instance = Load(out var modified);
            if (modified)
                Save();
        }


        [JsonProperty(nameof(Main))]
        private MainSettings _main;

        [JsonProperty(nameof(MainWindow))]
        private MainWindowSettings _settingsWindow;


        [JsonIgnore]
        public MainSettings Main => CheckSettingsPartExistAndSubscribe(ref _main);

        [JsonIgnore]
        public MainWindowSettings MainWindow => CheckSettingsPartExistAndSubscribe(ref _settingsWindow);

        public Settings() { }
        private Settings(bool defaultValues)
        {
            if (!defaultValues)
                return;
        }

        private T CheckSettingsPartExistAndSubscribe<T>(ref T part) where T : SettingsPartWithNotifier
        {
            if (part == null)
                part = Activator.CreateInstance<T>();
            var notifier = (ISettingsPartWithNotifier)part;
            if (!notifier.NotifierSubscribed)
                notifier.SubscribeNotifier(PartChanged);
            return part;
        }

        private void PartChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke();
        }

        private static Settings Load(out bool modified)
        {
            Settings result = null;
            modified = false;
            if (File.Exists(SettingsPath))
            {
                using (var file = new JsonTextReader(File.OpenText(SettingsPath)))
                {
                    var serializer = GetSerializer();
                    try
                    {
                        result = serializer.Deserialize<Settings>(file);
                        if (result == null)
                            throw new SerializationException($"cant read '{SettingsPath}'");
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show(e.Message, "settings deserialization fault");
                    }
                }
            }
            if (result == null)
            {
                result = new Settings(true);
                modified = true;
            }
            result.PropertyChanged += Save;
            return result;
        }

        public static void Save()
        {
            if (!Directory.Exists(AppDataPath))
                Directory.CreateDirectory(AppDataPath);

            using (var file = File.CreateText(SettingsPath))
            {
                var serializer = GetSerializer();
                try
                {
                    serializer.Serialize(file, Instance);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message, "settings serialization fault");
                }
            }
        }

        public static JsonSerializer GetSerializer()
        {
            if (_serializer == null)
            {
                _serializer = new JsonSerializer()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };
            }
            return _serializer;
        }
    }
}
