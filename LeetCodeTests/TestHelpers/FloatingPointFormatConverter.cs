using System;
using System.Globalization;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace LeetCodeTests {

    [PublicAPI]
    public class FloatingPointFormatConverter : JsonConverter {

        private readonly Int32 _precision;

        public FloatingPointFormatConverter()
            : this(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalDigits) {}

        public FloatingPointFormatConverter(Int32 precision) {
            this._precision = precision;
        }

        public override Boolean CanRead => false;
        public override Boolean CanConvert(Type objectType) => (objectType == typeof(Decimal)) || (objectType == typeof(Single)) || (objectType == typeof(Double));
        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer) => writer.WriteRawValue(String.Format(CultureInfo.InvariantCulture, $"{{0:F{this._precision}}}", value));

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

    }

}
