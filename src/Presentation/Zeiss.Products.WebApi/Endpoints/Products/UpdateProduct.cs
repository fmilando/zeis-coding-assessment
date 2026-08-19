using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class UpdateProduct
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher,
        ILogger logger,
        [FromRoute] int id,
        [FromBody] UpdateProductRequest request,
        HttpContext context)
    {
        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.Sku,
            request.Description,
            request.Price);

        var result = await dispatcher.DispatchAsync<UpdateProductCommand, UpdateProductResult>(
            command,
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsSuccess)
        {
            logger.Information("Updated product {ProductId}", id);
            return Results.Accepted($"{EndpointExtensions.BaseEndpoint}/{result.Value!.Product.ProductId}", response);
        }

        logger.Error("Failed update product {ProductId}: {Reason}", id, result.Errors);

        return Results.BadRequest(response);
    }
}