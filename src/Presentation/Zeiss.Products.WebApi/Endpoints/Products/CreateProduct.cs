using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Products.Commands.CreateProduct;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class CreateProduct
{
    public static async Task<IResult> HandleAsync(
        ISender sender,
        ILogger logger,
        [FromBody] CreateProductRequest request,
        HttpContext context)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Description,
            request.Price,
            request.Quantity);

        var result = await sender.Send(command, context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error(
                "Failed to create product with SKU '{SKU}': {Reason}",
                request.Sku,
                result.Errors);

            var isConflict = result.Errors.Any(x => x.Code == ErrorCodes.Product.SkuConflict);
            
            return isConflict
                ? Results.Conflict(response) 
                : Results.BadRequest(response);
        }

        logger.Information("Created product {ProductId}", result.Value!.ProductId);
        return Results.Created($"{EndpointExtensions.BaseEndpoint}/{result.Value!.ProductId}", response);
    }
}