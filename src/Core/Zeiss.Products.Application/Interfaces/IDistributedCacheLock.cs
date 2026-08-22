namespace Zeiss.Products.Application.Interfaces;

public interface IDistributedCacheLock
{
    Task<string?> GetValueAsync(
        string key,
        CancellationToken cancellationToken);

    Task SetValueAsync(
        string key,
        string value,
        TimeSpan timeout,
        CancellationToken cancellationToken);

    Task<bool> TryLockAsync(string key, CancellationToken cancellationToken);

    Task UnlockAsync(string key, CancellationToken cancellationToken);
}