using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Zeiss.Products.Application.Interfaces;
using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Helpers;

namespace Zeiss.Products.WebApi.Filters;

internal sealed class IdempotencyFilter(IIdempotencyGuard guard) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next
    )
    {
        var cancellationToken = context.HttpContext.RequestAborted;
        var userId = GetUserId(context.HttpContext.Request);
        var endpoint = HttpContextHelper.GetRequestEndpoint(context.HttpContext);
        var content = await GetRequestBodyAsync(
            context.HttpContext.Request, 
            context.HttpContext.RequestAborted);
        
        var fingerprint = GenerateRequestFingerprint(
            userId, 
            context.HttpContext.Request.Method, 
            endpoint, 
            content);
        
        var resultKey = $"idempotency:result:{fingerprint}";
        var lockKey = $"idempotency:lock:{fingerprint}";
        
        var cached = await guard.GetValueAsync(resultKey, cancellationToken);

        if (cached is not null)
        {
            return Results.Json(cached);
        }
        
        var (success, lockId) = await guard.TryLockAsync(lockKey, cancellationToken);

        if (success)
        {
            try
            { 
                var response = await next(context);

                if (response is not IStatusCodeHttpResult { StatusCode: StatusCodes.Status200OK })
                {
                    await guard.UnlockAsync(lockKey, lockId!.Value, cancellationToken);
                }
                else
                {
                    var cacheData = JsonSerializer.Serialize(response);
                    await guard.SetValueAsync(
                        resultKey, 
                        cacheData, 
                        TimeSpan.FromSeconds(30),
                        cancellationToken);
                }
                
                return response;
            }
            finally
            {
                await guard.UnlockAsync(lockKey, lockId!.Value, cancellationToken);
            }
        }

        var error = new Error(
            "DUPLICATE_REQUEST",
            "An identical request is being processed.");
        
        var result = new Result<string>([error]);
        return Results.Conflict(result);
    }

    private static async Task<string> GetRequestBodyAsync(
        HttpRequest request, 
        CancellationToken cancellationToken)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var content = await reader.ReadToEndAsync(cancellationToken);
        request.Body.Position = 0;
        
        return content;
    }

    private static string GetUserId(HttpRequest request)
    {
        var token = request.Headers.Authorization.FirstOrDefault(x => 
            x is null || x.StartsWith("bearer", StringComparison.OrdinalIgnoreCase)
        ) ?? "anonymous";
        
        return token.Split(" ").Last();
    }

    private static string GenerateRequestFingerprint(
        string userId,
        string httpMethod,
        string requestPath,
        string requestContent)
    {
        var data = $"{userId}.{httpMethod}.{requestPath}.{requestContent}";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(bytes);
    }
}