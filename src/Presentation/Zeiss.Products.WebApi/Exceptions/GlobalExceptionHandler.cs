using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Helpers;
using Zeiss.Products.WebApi.Mappers;
namespace Zeiss.Products.WebApi.Exceptions;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger ) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var endpoint = HttpContextHelper.GetRequestEndpoint(httpContext);
        logger.LogError(exception, "Error processing request to '{Endpoint}'", endpoint);
        
        var error = new Error(
            ErrorCodes.UnexpectedError,
            "Request could not be processed due to an unexpected error.");

        var response = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(error.ToApiResponse(false))
        );
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.Body.WriteAsync(response, cancellationToken);
        
        return true;
    }
}