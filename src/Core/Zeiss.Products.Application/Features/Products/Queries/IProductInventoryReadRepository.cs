using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries;

public interface IProductInventoryReadRepository
{
    Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> GetAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<ProductInventoryReadModel?> GetByIdAsync(
        long productId,
        CancellationToken cancellationToken);

    Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> GetByStockLevelAsync(
        int? minStock,
        int? maxStock,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> SearchByNameAsync(
        string text,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}