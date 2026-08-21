using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Features.Products.Queries.GetProductById;
using Zeiss.Products.WebApi.Mappers;
using ILogger = Serilog.ILogger;

namespace Zeiss.Products.WebApi.Endpoints.Products;

internal static class GetProductById
{
    public static async Task<IResult> HandleAsync(
        ISender sender,
        ILogger logger,
        [FromRoute] int id,
        HttpContext context)
    {
        var query = new GetProductByIdQuery(id);
        var result = await sender.Send(query, context.RequestAborted);

        var response = result.ToApiResponse();

        if (result.IsSuccess)
        {
            return Results.Ok(response);
        }

        logger.Error("Failed to get product by id {ProductId}: {Reason}", id, result.Errors);
        
        var isNotFound = result.Errors.Any(x => x.Code == ErrorCodes.Product.NotFound);
        
        return isNotFound 
            ? Results.NotFound(response) 
            : Results.BadRequest(response);
    }
}