namespace Zeiss.Products.WebApi.Helpers;

internal static class HttpContextHelper
{
    public static string GetRequestEndpoint(HttpContext context)
    {
        var request = context.Request;
        var scheme = request.Scheme;
        var host = request.Host;
        var path = request.Path;
        var query = request.QueryString.Value;

        return $"{scheme}://{host}{path}{query}";
    }
}