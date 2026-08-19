namespace Zeiss.Products.Application.Interfaces;

public interface IIdempotencyGuard
{
    Task<string?> GetValueAsync(
        string key, 
        CancellationToken cancellationToken);
    
    Task SetValueAsync(
        string key, 
        string value, 
        TimeSpan timeout, 
        CancellationToken cancellationToken);
    
    Task<(bool Success, Guid? LockId)> TryLockAsync(string key, CancellationToken cancellationToken);
    
    Task UnlockAsync(string key, Guid lockId, CancellationToken cancellationToken);
}