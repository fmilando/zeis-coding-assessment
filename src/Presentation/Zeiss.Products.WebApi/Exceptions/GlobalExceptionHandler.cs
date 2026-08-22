using System.Text;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Exceptions;
using Zeiss.Products.WebApi.Mappers;
namespace Zeiss.Products.WebApi.Exceptions;

internal sealed class GlobalExceptionHandler(
    JsonSerializerOptions options,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var endpoint = GetRequestEndpoint(httpContext);
        logger.LogError(exception, "Error processing request to '{Endpoint}'", endpoint);
        
        var statusCode = exception switch
        {
            DomainException or ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        var response = Encoding.UTF8.GetBytes(GetResponse(exception));
        await httpContext.Response.Body.WriteAsync(response, cancellationToken);
        
        return true;
    }

    private string GetResponse(Exception exception)
    {
        var errors = exception switch
        {
            ValidationException ex => ex.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage)),
            DomainException ex => [new Error(ex.Code, ex.Message)],
            _ => [new Error(ErrorCodes.UnexpectedError, "Request could not be processed due to an unexpected error.")]
        };
        
        var response = errors.ToApiResponse(false);
        return JsonSerializer.Serialize(response, options: options);
    }
    
    private static string GetRequestEndpoint(HttpContext context)
    {
        var request = context.Request;
        var scheme = request.Scheme;
        var host = request.Host;
        var path = request.Path;
        var query = request.QueryString.Value;

        return $"{scheme}://{host}{path}{query}";
    }
}