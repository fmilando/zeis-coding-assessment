using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Mappers;

namespace Zeiss.Products.Infrastructure.Repositories;

internal sealed class InventoryRepository(PersistenceDbContext context) : IInventoryRepository
{
    private volatile IDbContextTransaction? _transaction;

    public async Task<Inventory?> GetAsync(long productId, CancellationToken cancellationToken)
    {
        var entity = await context.Inventory.FirstOrDefaultAsync(
            x => x.ProductId == productId,
            cancellationToken);
        
        return InventoryEntityMapper.Map(entity);
    }

    public async Task<Inventory> AddAsync(Inventory inventory, CancellationToken cancellationToken)
    {
        var entity = InventoryEntityMapper.Map(inventory);
        context.Inventory.Add(entity);
        
        var result = await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return InventoryEntityMapper.Map(result.Entity)!;
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
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            throw new InvalidOperationException("The transaction already started.");
        }
        
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            await context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            _transaction = null;
        }
    }
    
    public async Task DiscardAsync(CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            _transaction = null;
        }
    }
}