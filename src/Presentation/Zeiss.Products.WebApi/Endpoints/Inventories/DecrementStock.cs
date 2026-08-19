using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Inventories;

internal static class DecrementStock
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [FromRoute] int id,
        [FromRoute] int quantity,
        HttpContext context)
    {
        var command = new DecrementStockCommand(id, quantity);

        var result = await dispatcher.DispatchAsync<DecrementStockCommand, DecrementStockResult>(
            command,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error(
                "Failed to decrement the stock of product {ProductId} by {Quantity}: {Reason}",
                id,
                quantity,
                result.Errors);

            return Results.BadRequest(response);
        }

        logger.Information("Decremented the stock of product {ProductId} by {Quantity}", id, quantity);
        return Results.Ok(response);
    }
}