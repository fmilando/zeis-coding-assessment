using Zeiss.Products.WebApi.Filters;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        const string productsTag = "products";
        
        var endpoints = routes.MapGroup(EndpointExtensions.BaseEndpoint);
        endpoints.RequireAuthorization();

        endpoints.MapGet("/", GetProducts.HandleAsync).WithTags(productsTag);
        endpoints.MapGet("/{id:int}", GetProductById.HandleAsync).WithTags(productsTag);
        endpoints.MapGet("/search", SearchProducts.HandleAsync).WithTags(productsTag);
        endpoints.MapGet("/stock-level", GetByStockLevel.HandleAsync).WithTags(productsTag);
        
        endpoints.MapPost("/", CreateProduct.HandleAsync)
                 .WithTags(productsTag)
                 .WithIdempotencyCheck();
        
        endpoints.MapPut("/{id:int}", UpdateProduct.HandleAsync).WithTags(productsTag);
        
        endpoints.MapDelete("/{id:int}", DeleteProduct.HandleAsync).WithTags(productsTag);
    }
}