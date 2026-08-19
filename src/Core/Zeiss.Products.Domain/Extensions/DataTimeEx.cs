namespace Zeiss.Products.Domain.Extensions;

public static class DataTimeEx
{
    public static string ToIso8601(this DateTime dateTime)
        => dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}