using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class UpdateProduct
{
    public static async Task<IResult> HandleAsync(
        ISender sender,
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

        var result = await sender.Send(command, context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsSuccess)
        {
            logger.Information("Updated product {ProductId}", id);
            return Results.Accepted($"{EndpointExtensions.BaseEndpoint}/{result.Value!.ProductId}", response);
        }

        logger.Error("Failed update product {ProductId}: {Reason}", id, result.Errors);

        var isNotFound = result.Errors.Any(x => x.Code == ErrorCodes.Product.NotFound);
        var isUnchanged = result.Errors.Any(x => x.Code == ErrorCodes.Product.Unchanged);

        return (isNotFound, isUnchanged) switch
        {
            (true,_) => Results.NotFound(response),
            (_,true) => Results.Ok(response),
            _ => Results.BadRequest(response)
        };
    }
}