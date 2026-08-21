using Microsoft.EntityFrameworkCore;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Mappers;

namespace Zeiss.Products.Infrastructure.Database.Repositories;

internal sealed class InventoryRepository(PersistenceDbContext context) : IInventoryRepository
{
    public async Task<Inventory?> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        var entity = await context.Inventory.FirstOrDefaultAsync(
            x => x.ProductId == productId,
            cancellationToken);

        return entity.ToDomainEntity();
    }

    public async Task<Inventory> AddAsync(Inventory inventory, CancellationToken cancellationToken)
    {
        var entity = inventory.ToEntity()!;
        context.Inventory.Add(entity);

        var result = await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return result.Entity.ToDomainEntity()!;
    }

    public async Task<Inventory> UpdateAsync(Inventory inventory, CancellationToken cancellationToken)
    {
        await context.Inventory
            .Where(x => x.Id == inventory.Id)
            .ExecuteUpdateAsync(item =>
            {
                item.SetProperty(p => p.Quantity, inventory.Quantity)
                    .SetProperty(p => p.UpdatedAt, inventory.UpdatedAt);
            }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return inventory;
    }
}