using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Interfaces.Repositories;

public interface IInventoryRepository : IUnitOfWork
{
    Task<Inventory?> GetAsync(long productId, CancellationToken cancellationToken);
    Task<Inventory> AddAsync(Inventory inventory, CancellationToken cancellationToken);
    Task<Inventory> UpdateAsync(Inventory inventory, CancellationToken cancellationToken);
}