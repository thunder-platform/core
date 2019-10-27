using System.Text.Json;

namespace Thunder.Platform.Core.Json
{
    public static class ThunderJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions SharedJsonSerializerOptions;

        static ThunderJsonSerializerOptions()
        {
            SharedJsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };

            SharedJsonSerializerOptions.Converters.Add(new ThunderDateTimeConverter());
            SharedJsonSerializerOptions.Converters.Add(new ThunderNullableDateTimeConverter());
        }
    }
}
