using Zeiss.Products.WebApi.Endpoints.Inventories;
using Zeiss.Products.WebApi.Endpoints.Products;
using Zeiss.Products.WebApi.Endpoints.Tokens;

namespace Zeiss.Products.WebApi.Endpoints;

internal static class EndpointExtensions
{
    public const string BaseEndpoint = "/api/products";

    public static void MapApiEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapProductEndpoints();
        routes.MapInventoryEndpoints();

        routes.MapPost("/api/auth", GetAccessToken.HandleAsync)
            .WithTags("access-token")
            .AllowAnonymous();
    }
}