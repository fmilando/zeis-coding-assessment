using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features.Products.Commands.CreateProduct;
using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.WebApi.Contracts;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class CreateProduct
{
    public static async Task<IResult> HandleAsync(
        IRequestDispatcher dispatcher, 
        ILogger logger,
        [FromBody] CreateProductRequest request,
        HttpContext context)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Description,
            request.Price);
        
        var result = await dispatcher.DispatchAsync<CreateProductCommand, CreateProductResult>(
            command, 
            context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsError)
        {
            logger.Error(
                "Failed to create product with SKU '{SKU}': {Reason}", 
                request.Sku,
                result.Errors);
            
            return Results.BadRequest(response);
        }

        logger.Information("Created product {ProductId}", result.Value!.Product.ProductId);
        return Results.Created($"{EndpointExtensions.BaseEndpoint}/{result.Value!.Product.ProductId}", response);
    }
}