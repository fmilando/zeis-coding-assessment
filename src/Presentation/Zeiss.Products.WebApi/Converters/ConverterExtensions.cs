namespace Zeiss.Products.WebApi.Converters;

internal static class ConverterExtensions
{
    public static void AddDefaultJsonDateTimeConverter(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(
            options => options.SerializerOptions.Converters.Add(new DefaultJsonDateTimeConverter())
        );
    }
}