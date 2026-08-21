namespace Zeiss.Products.Domain.Entities;

public sealed record Account(
    int Id, 
    string ClientId, 
    string ClientSecret,
    bool IsLocked,
    DateTime CreatedAt,
    DateTime UpdatedAt) : Entity<int>(Id);