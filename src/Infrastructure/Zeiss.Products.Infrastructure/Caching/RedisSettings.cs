namespace Zeiss.Products.Infrastructure.Caching;

internal class RedisSettings
{
    public const string SectionName = "Redis";
    public required string ConnectionString { get; set; }
    public required int IdempotencyLockRetentionInSeconds { get; set; }
}