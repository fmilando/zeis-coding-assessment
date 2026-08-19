using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class DeleteProduct
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [FromRoute] int id,
        HttpContext context)
    {
        var command = new DeleteProductCommand(id);

        var result = await dispatcher.DispatchAsync<DeleteProductCommand, DeleteProductResult>(
            command,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error("Failed to delete product {ProductId}: {Reason}", id, result.Errors);
            return Results.BadRequest(response);
        }

        logger.Information("Deleted product {ProductId}", id);
        return Results.NoContent();
    }
}