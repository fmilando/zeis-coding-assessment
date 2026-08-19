using Zeiss.Products.Application.Features.Products.Queries.GetProducts;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class GetProducts
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [AsParameters] PageRequest request,
        HttpContext context)
    {
        var query = new GetProductsQuery
        {
            PageNumber = request.Page ?? 1,
            PageSize = request.PageSize ?? ResponseConstants.DefaultPageSize
        };

        var result = await dispatcher.DispatchAsync<GetProductsQuery, GetProductsResult>(
            query,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsSuccess)
        {
            return Results.Ok(response);
        }

        logger.Error("Failed to get products with {Query}: {Reason}", request, result.Errors);

        return Results.BadRequest(response);
    }
}