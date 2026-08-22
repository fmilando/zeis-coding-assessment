namespace Zeiss.Products.WebApi.Filters;

internal static class FilterExtensions
{
    public static void WithIdempotency(this RouteHandlerBuilder route)
    {
        route.AddEndpointFilter<IdempotencyFilter>();
    }
}