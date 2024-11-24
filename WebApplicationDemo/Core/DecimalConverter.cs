using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplicationDemo.Core;

public class DecimalConverter : JsonConverter<decimal>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type to convert the JSON to.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <returns>The converted <see cref="decimal"/> value.</returns>
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

    /// <summary>
    /// Writes a decimal value to the provided Utf8JsonWriter.
    /// </summary>
    /// <param name="writer">The Utf8JsonWriter to which the value will be written.</param>
    /// <param name="value">The decimal value to write.</param>
    /// <param name="options">Options to control the serialization behavior.</param>
    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
