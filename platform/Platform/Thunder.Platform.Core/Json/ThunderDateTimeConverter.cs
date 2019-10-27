using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.Core.Json
{
    public class ThunderDateTimeConverter : JsonConverter<DateTime>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Clock.Normalize(reader.GetDateTime());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(Clock.Normalize(value));
        }
    }
}
