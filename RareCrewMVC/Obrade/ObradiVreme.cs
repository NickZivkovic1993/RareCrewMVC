using Newtonsoft.Json;

namespace RareCrewMVC.Obrade
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return TimeSpan.Parse((string)reader.Value);
            }
            throw new JsonException($"Unexpected token type '{reader.TokenType}' when parsing TimeSpan.");
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("c"));
        }
    }
}
