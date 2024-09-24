using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleDemo.Core;

public class DecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (decimal.TryParse(reader.GetString(), out var value))
            {
                return value;
            }
        }
        return reader.GetDecimal();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}