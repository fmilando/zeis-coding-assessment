using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Zeiss.Products.Application.Features;
using Zeiss.Products.Application.Interfaces;
using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Helpers;
using Zeiss.Products.WebApi.Security;

namespace Zeiss.Products.WebApi.Filters;

internal sealed class IdempotencyFilter(IIdempotencyGuard guard) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        //TODO: Fix THIS
        return await next(context);
        
        var cancellationToken = context.HttpContext.RequestAborted;
        var userId = GetUserUniqueId(context.HttpContext.User);
        var endpoint = HttpContextHelper.GetRequestEndpoint(context.HttpContext);
        var content = await GetRequestBodyAsync(
            context.HttpContext.Request,
            context.HttpContext.RequestAborted);

        var fingerprint = GenerateRequestFingerprint(
            userId,
            context.HttpContext.Request.Method,
            endpoint,
            content);

        var lockKey = $"idempotency:lock:{fingerprint}";
        var resultKey = $"idempotency:result:{fingerprint}";
        
        var requestStatus = await guard.GetValueAsync(resultKey, cancellationToken);
        
        var (success, lockId) = await guard.TryLockAsync(lockKey, cancellationToken);

        if (success)
        {
            try
            {
                var response = await next(context);

                if (response is not IStatusCodeHttpResult { StatusCode: >= 200 and <= 299 })
                {
                    await guard.UnlockAsync(lockKey, lockId!.Value, cancellationToken);
                }
                
                return response;
            }
            catch
            {
                await guard.UnlockAsync(lockKey, lockId!.Value, cancellationToken);
                throw;
            }
        }

        var error = new Error(
            ErrorCodes.DuplicateRequest,
            "An identical request is being processed.");

        Result<string> result = error;
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

    private static string GetUserUniqueId(ClaimsPrincipal user)
    {
        return user.Claims.First(x => x.Type == JwtSettings.UserUniqueIdClaimName).Value;
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