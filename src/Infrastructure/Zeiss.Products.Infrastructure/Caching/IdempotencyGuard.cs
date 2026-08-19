using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Zeiss.Products.Application.Interfaces;

namespace Zeiss.Products.Infrastructure.Caching;

internal sealed class IdempotencyGuard(
    IConnectionMultiplexer redis, 
    IOptions<RedisSettings> settings,
    ILogger<IdempotencyGuard> logger) : IIdempotencyGuard
{
    public async Task<string?> GetValueAsync(string key, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var cache = redis.GetDatabase();
        return await cache.StringGetAsync(key);
    }

    public async Task SetValueAsync(string key, string value, TimeSpan timeout, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var cache = redis.GetDatabase();
        await cache.StringSetAsync(key, value, timeout);
    }

    public async Task<(bool Success, Guid? LockId)> TryLockAsync(string key, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var cache = redis.GetDatabase();
        var lockId = Guid.NewGuid();
        var success = await cache.LockTakeAsync(
            key, 
            lockId.ToString(),
            TimeSpan.FromSeconds(settings.Value.RecordRetentionInSeconds));

        if (success is false)
        {
            logger.LogInformation("Failed to lock {key}", key);
        }
        
        return (success, success ? lockId : null);
    }

    public async Task UnlockAsync(string key, Guid lockId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var cache = redis.GetDatabase();
        await cache.LockReleaseAsync(key, lockId.ToString(), CommandFlags.FireAndForget);
    }
}