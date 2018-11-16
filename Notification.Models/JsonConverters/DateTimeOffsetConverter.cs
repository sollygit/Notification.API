using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Notification.Models.JsonConverters
{
    public class DateTimeOffsetConverter : JsonConverter
    {
        private Regex hasOffset = new Regex(@"Z|\+\d{2}:\d{2}$");
        private readonly Regex hasTime = new Regex(@"T\d{2}:\d{2}");
        private TimeZoneInfo timeZone;

        public DateTimeOffsetConverter(string timeZoneId)
        {
            timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException("Could not convert string to DateTimeOffset");
            var stringValue = serializer.Deserialize(reader, typeof(string)) as string;
            if (string.IsNullOrWhiteSpace(stringValue))
                return null;
            // if the date already has an offset, use it
            if (hasOffset.IsMatch(stringValue))
                return DateTimeOffset.Parse(stringValue);
            // otherwise set it to be the timezone offset (should be ok without daylight savings)
            var date = DateTime.Parse(stringValue);
            return new DateTimeOffset(date, timeZone.BaseUtcOffset);
        }

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value as DateTimeOffset?);
        }
    }
}
