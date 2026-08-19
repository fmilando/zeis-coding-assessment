using Microsoft.EntityFrameworkCore;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Mappers;

namespace Zeiss.Products.Infrastructure.Repositories;

internal sealed class ProductRepository(PersistenceDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await context.Products.FindAsync(id, cancellationToken);
        return ProductEntityMapper.Map(entity);
    }

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        var entity = await context.Products.FirstOrDefaultAsync(
            x => x.Sku == sku,
            cancellationToken);
        return ProductEntityMapper.Map(entity);
    }

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
    {
        var entity = ProductEntityMapper.Map(product);
        context.Products.Add(entity);
        
        var result = await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return ProductEntityMapper.Map(result.Entity)!;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products
            .Where(x => x.Id == product.Id)
            .ExecuteUpdateAsync(prod =>
            {
                prod.SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Sku, product.Sku)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.UpdatedAt, product.UpdatedAt);
            }, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products
            .Where(x => x.Id == product.Id)
            .ExecuteUpdateAsync(prod =>
            {
                prod.SetProperty(p => p.IsActive, product.IsActive)
                    .SetProperty(p => p.IsDeleted, product.IsDeleted)
                    .SetProperty(p => p.DeletedAt, product.DeletedAt);
            }, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}