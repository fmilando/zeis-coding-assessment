namespace Zeiss.Products.Infrastructure.Database.Entities;

internal sealed class AccountEntity
{
    public required int Id { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required bool IsLocked { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
}