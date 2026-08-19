using Dapper;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Domain.Common;
using Zeiss.Products.Infrastructure.Database;

namespace Zeiss.Products.Infrastructure.Repositories;

internal sealed class ProductInventoryReadRepository(
    IDbConnectionFactory connectionFactory
) : IProductInventoryReadRepository
{
    public async Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> GetAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var connection = connectionFactory.Create();
        connection.Open();

        const string countQuery = """SELECT COUNT(1) FROM "Products" """;

        const string query = $"""
                     {BaseQuery}
                     LIMIT @pageSize OFFSET @page;
                     """;

        var totalItems = await connection.ExecuteScalarAsync<int>(countQuery);
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var items = (await connection.QueryAsync<ProductInventoryReadModel>(
            query, new
            {
                page = (pageNumber - 1) * pageSize,
                pageSize,
            })).ToList();

        var pagedResult = new PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>(
            items.AsReadOnly(),
            new PaginationInfo(pageNumber, pageSize, totalPages, totalItems)
        );

        return pagedResult;
    }

    public async Task<ProductInventoryReadModel?> GetByIdAsync(
        long productId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var connection = connectionFactory.Create();
        connection.Open();

        const string query = $"""
                              {BaseQuery}
                              AND p."Id" = @productId;
                              """;

        var items = (await connection.QueryAsync<ProductInventoryReadModel>(
            query, new
            {
                productId
            })).ToList();

        return items.FirstOrDefault();
    }

    public async Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> GetByStockLevelAsync(
        int? minStock,
        int? maxStock,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var connection = connectionFactory.Create();
        connection.Open();

        const string criteria = """
                                i."Quantity" BETWEEN COALESCE(@minStock, i."Quantity") AND COALESCE(@maxStock, i."Quantity")
                                """;
        const string countQuery = $""""
                                  SELECT COUNT(1) FROM "Inventory" i
                                  WHERE {criteria};
                                  """";

        const string query = $"""
                              {BaseQuery}
                              AND {criteria} 
                              LIMIT @pageSize OFFSET @page;
                              """;

        var totalItems = await connection.ExecuteScalarAsync<int>(countQuery, new
        {
            minStock,
            maxStock,
        });

        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var items = (await connection.QueryAsync<ProductInventoryReadModel>(
            query, new
            {
                minStock,
                maxStock,
                page = (pageNumber - 1) * pageSize,
                pageSize,
            })).ToList();

        var pagedResult = new PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>(
            items.AsReadOnly(),
            new PaginationInfo(pageNumber, pageSize, totalPages, totalItems)
        );

        return pagedResult;
    }

    public async Task<PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>> SearchByNameAsync(
        string text,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var connection = connectionFactory.Create();
        connection.Open();

        const string criteria = """UPPER("Name") LIKE UPPER(@pattern)""";
        const string countQuery = $"""SELECT COUNT(1) FROM "Products" WHERE {criteria};""";

        const string query = $"""
                              {BaseQuery}
                              AND {criteria}
                              LIMIT @pageSize OFFSET @page;
                              """;

        var totalItems = await connection.ExecuteScalarAsync<int>(countQuery, new
        {
            pattern = $"%{text}%"
        });

        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var items = (await connection.QueryAsync<ProductInventoryReadModel>(
            query, new
            {
                pattern = $"%{text}%",
                page = (pageNumber - 1) * pageSize,
                pageSize,
            })).ToList();

        var pagedResult = new PagedResult<IReadOnlyCollection<ProductInventoryReadModel>>(
            items.AsReadOnly(),
            new PaginationInfo(pageNumber, pageSize, totalPages, totalItems)
        );

        return pagedResult;
    }

    private const string BaseQuery = """"
                                     SELECT 
                                     	p."Id" as "ProductId",
                                     	p."Name",
                                     	p."Sku",
                                     	p."Description",
                                     	p."Price",
                                     	p."IsActive",
                                     	p."IsDeleted",
                                     	p."CreatedAt",
                                     	p."UpdatedAt",
                                     	p."DeletedAt",
                                     	i."Quantity" as "QuantityInStock",
                                     	GREATEST(i."CreatedAt", i."UpdatedAt") AS "StockUpdatedAt"
                                     FROM "Products" p
                                     LEFT JOIN "Inventory" i ON p."Id" = i."ProductId"
                                     WHERE p."IsDeleted" IS DISTINCT FROM true
                                     """";
}