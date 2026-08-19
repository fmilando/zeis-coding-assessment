namespace Zeiss.Products.WebApi.Filters;

internal static class FilterExtensions
{
    public static void WithIdempotencyCheck(this RouteHandlerBuilder route)
    {
        route.AddEndpointFilter<IdempotencyFilter>();
    }
}