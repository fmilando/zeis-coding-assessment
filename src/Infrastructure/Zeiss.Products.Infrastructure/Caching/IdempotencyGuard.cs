using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Zeiss.Products.Application.Interfaces;

namespace Zeiss.Products.Infrastructure.Caching;

internal sealed class IdempotencyGuard(
    IConnectionMultiplexer redis,
    ILogger<IdempotencyGuard> logger) : IIdempotencyGuard
{
    private readonly IDatabase _cache = redis.GetDatabase();
    
    public async Task<string?> GetValueAsync(string key, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _cache.StringGetAsync(key);
    }

    public async Task SetValueAsync(string key, string value, TimeSpan timeout, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _cache.StringSetAsync(key, value, timeout);
    }

    public async Task<bool> TryLockAsync(string key, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var success = await _cache.LockTakeAsync(
            key,
            "true",
            TimeSpan.FromHours(24));

        if (success is false)
        {
            logger.LogInformation("Failed to lock {key}", key);
        }

        return success;
    }

    public async Task UnlockAsync(string key, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var lockValue = await _cache.LockQueryAsync(key);
        await _cache.LockReleaseAsync(key, lockValue);
    }
}