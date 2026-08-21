using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetAsync(int id, CancellationToken cancellationToken);
    Task<Product> AddAsync(Product entity, CancellationToken cancellationToken);
    Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken);
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
}