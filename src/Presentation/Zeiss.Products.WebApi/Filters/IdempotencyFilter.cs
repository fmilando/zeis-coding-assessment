using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Interfaces;
using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Mappers;

namespace Zeiss.Products.WebApi.Filters;

internal sealed class IdempotencyFilter(
    IDistributedCacheLock cacheLock,
    JsonSerializerOptions serializerOptions) : IEndpointFilter
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
        
        var fingerprint = GetFingerprint(context.HttpContext.Request.Path, idempotencyKey!);
        var cancellationToken = context.HttpContext.RequestAborted;
        var lockKey = $"idempotency:lock:{fingerprint}";
        
        var success = await cacheLock.TryLockAsync(lockKey, cancellationToken);

        if (success is false)
        {
            var error = new Error(
                ErrorCodes.DuplicateRequest,
                $"A request with the same {IdempotencyHeaderName} is being processed.");
            return Results.Conflict(error.ToApiResponse(false));
        }
        
        var resultKey = $"idempotency:result:{fingerprint}";
        var result = await cacheLock.GetValueAsync(resultKey, cancellationToken);

        if (result is not null)
        {
            context.HttpContext.Response.Headers["Idempotency-Key-Cache-Hit"] = "true";
            var cached = JsonSerializer.Deserialize<object>(result)!;
            await cacheLock.UnlockAsync(lockKey, cancellationToken);
            
            return cached;
        }
        
        try
        {
            var response = await next(context);
            
            if (response is IStatusCodeHttpResult { StatusCode: >= 200 and <= 299 } and IValueHttpResult httpResult)
            {
                await cacheLock.SetValueAsync(
                    resultKey, 
                    JsonSerializer.Serialize(httpResult.Value, options: serializerOptions), 
                    TimeSpan.FromHours(24),
                    cancellationToken);
            }
            
            return response;
        }
        finally
        {
            await cacheLock.UnlockAsync(lockKey, cancellationToken);
        }
    }

    private static string GetFingerprint(string path, string idempotencyKey)
    {
        var bytes = Encoding.UTF8.GetBytes($"{path}:{idempotencyKey}");
        return Convert.ToBase64String(bytes);
    }
}