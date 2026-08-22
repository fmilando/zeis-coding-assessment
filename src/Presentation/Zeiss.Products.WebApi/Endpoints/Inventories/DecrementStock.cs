using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Inventories;

internal static class DecrementStock
{
    public static async Task<IResult> HandleAsync(
        ISender sender,
        ILogger logger,
        [FromRoute] int id,
        [FromRoute] int quantity,
        HttpContext context)
    {
        var command = new DecrementStockCommand(id, quantity);

        var result = await sender.Send(command, context.RequestAborted);
        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error(
                "Failed to decrement the stock of product {ProductId} by {Quantity}: {Reason}",
                id,
                quantity,
                result.Errors);
            
            var isNotFound = result.Errors.Any(x => x.Code == ErrorCodes.Product.NotFound);
            var isExceeded = result.Errors.Any(x => x.Code == ErrorCodes.Inventory.QuantityExceeded);
            
            return (isNotFound, isExceeded) switch
            {
                (true,_) => Results.NotFound(response),
                (_, true) => Results.Conflict(response),
                _ => Results.BadRequest(response)
            };
        }

        logger.Information("Decremented the stock of product {ProductId} by {Quantity}", id, quantity);
        return Results.Ok(response);
    }
}