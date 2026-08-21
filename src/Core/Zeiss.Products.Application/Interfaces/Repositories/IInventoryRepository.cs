using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Interfaces.Repositories;

public interface IInventoryRepository
{
    Task<Inventory?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    Task<Inventory> AddAsync(Inventory entity, CancellationToken cancellationToken);
    Task<Inventory> UpdateAsync(Inventory entity, CancellationToken cancellationToken);
}