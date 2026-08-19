using Zeiss.Products.WebApi.Filters;

namespace Zeiss.Products.WebApi.Endpoints.Inventories;

internal static class InventoryEndpoints
{
    public static void MapInventoryEndpoints(this IEndpointRouteBuilder routes)
    {
        const string inventoryTag = "inventory";
        var endpoints = routes.MapGroup(EndpointExtensions.BaseEndpoint + "/{id:int}");
        endpoints.RequireAuthorization();

        endpoints.MapPost("/decrement-stock/{quantity:int}", DecrementStock.HandleAsync)
                 .WithTags(inventoryTag)
                 .WithIdempotencyCheck();
        
        endpoints.MapPost("/add-to-stock/{quantity:int}", AddToStock.HandleAsync)
                 .WithTags(inventoryTag)
                 .WithIdempotencyCheck();
    }
}