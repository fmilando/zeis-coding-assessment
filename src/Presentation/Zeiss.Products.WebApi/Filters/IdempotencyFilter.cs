using System.Text.Json;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Interfaces;
using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Mappers;

namespace Zeiss.Products.WebApi.Filters;

internal sealed class IdempotencyFilter(IIdempotencyGuard guard) : IEndpointFilter
{
    private const string IdempotencyHeaderName = "Idempotency-Key";
    
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var idempotencyKey = context.HttpContext.Request.Headers[IdempotencyHeaderName];

        if (string.IsNullOrEmpty(idempotencyKey) || Guid.TryParse(idempotencyKey, out _) is false)
        {
            var error = new Error(
                ErrorCodes.MissingIdempotencyKey,
                $"Missing the required GUID value from {IdempotencyHeaderName} header.");
            
            return Results.BadRequest(error.ToApiResponse(false));
        }
        
        var lockKey = $"idempotency:lock:{idempotencyKey}";
        var cancellationToken = context.HttpContext.RequestAborted;
        
        var success = await guard.TryLockAsync(lockKey, cancellationToken);

        if (success is false)
        {
            var error = new Error(ErrorCodes.DuplicateRequest, "An identical request is being processed.");
            return Results.Conflict(error.ToApiResponse(false));
        }
        
        var resultKey = $"idempotency:result:{idempotencyKey}";
        var result = await guard.GetValueAsync(resultKey, cancellationToken);

        if (result is not null)
        {
            context.HttpContext.Response.Headers["Idempotency-Key-Cache-Hit"] = "true";
            var cached = JsonSerializer.Deserialize<object>(result)!;
            await guard.UnlockAsync(lockKey, cancellationToken);
            
            return cached;
        }
        
        try
        {
            var response = await next(context);
            
            if (response is IStatusCodeHttpResult { StatusCode: >= 200 and <= 299 } and IValueHttpResult httpResult)
            {
                await guard.SetValueAsync(
                    resultKey, 
                    JsonSerializer.Serialize(httpResult.Value), 
                    TimeSpan.FromHours(24),
                    cancellationToken);
            }
            
            return response;
        }
        finally
        {
            await guard.UnlockAsync(lockKey, cancellationToken);
        }
    }
}