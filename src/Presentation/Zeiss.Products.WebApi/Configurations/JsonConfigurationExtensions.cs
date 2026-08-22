using System.Text.Encodings.Web;
using System.Text.Json;

namespace Zeiss.Products.WebApi.Configurations;

internal static class JsonConfigurationExtensions
{
    public static void AddJsonSerializationOptions(this IServiceCollection services)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        
        services.AddSingleton(options);
        services.ConfigureHttpJsonOptions(json =>
        {
            json.SerializerOptions.Encoder = options.Encoder;
            json.SerializerOptions.PropertyNamingPolicy = options.PropertyNamingPolicy;
            json.SerializerOptions.DictionaryKeyPolicy = options.DictionaryKeyPolicy;
        });
    }
}