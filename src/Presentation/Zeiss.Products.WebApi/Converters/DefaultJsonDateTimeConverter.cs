using System.Text.Json;
using System.Text.Json.Serialization;
using Zeiss.Products.Domain.Extensions;

namespace Zeiss.Products.WebApi.Converters;

internal class DefaultJsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) => DateTime.Parse(reader.GetString()!);

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.ToIso8601());
}