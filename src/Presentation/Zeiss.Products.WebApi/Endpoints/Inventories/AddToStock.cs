using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Inventories;

internal static class AddToStock
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [FromRoute] int id,
        [FromRoute] int quantity,
        HttpContext context)
    {
        var command = new AddToStockCommand(id, quantity);

        var result = await dispatcher.DispatchAsync<AddToStockCommand, AddToStockResult>(
            command,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error("Failed to increment the stock of product {ProductId} by {Quantity}: {Reason}",
                id,
                quantity,
                result.Errors);
            return Results.BadRequest(response);
        }

        logger.Information("Added {Quantity} to stock for product {ProductId}", quantity, id);
        return Results.Ok(response);
    }
}