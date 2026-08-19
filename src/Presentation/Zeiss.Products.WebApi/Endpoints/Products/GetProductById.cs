using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Products.Queries.GetProductById;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class GetProductById
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [FromRoute] int id,
        HttpContext context)
    {
        var query = new GetProductByIdQuery(id);
        var result = await dispatcher.DispatchAsync<GetProductByIdQuery, GetProductByIdResult>(
            query,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsSuccess)
        {
            return Results.Ok(response);
        }

        logger.Error("Failed to get product by id {ProductId}: {Reason}", id, result.Errors);

        return Results.BadRequest(response);
    }
}