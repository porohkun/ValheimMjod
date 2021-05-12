using Newtonsoft.Json;
using System;

namespace ValheimMjod
{
    public class VersionConverter : JsonConverter<Version>
    {
        public static VersionConverter Default { get; } = new VersionConverter();

        public override Version ReadJson(JsonReader reader, Type objectType, Version existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var data = (string)reader.Value;
            return Version.TryParse(data, out var r) ? r : new Version();
        }

        public override void WriteJson(JsonWriter writer, Version value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
