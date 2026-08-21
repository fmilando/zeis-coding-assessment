using System.Data;
using System.Globalization;
using Bogus;
using Dapper;
using Microsoft.Data.Sqlite;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Database.Repositories;

namespace Zeiss.Products.UnitTests.Infrastructure.Repositories;

public sealed class ProductInventoryReadRepositoryTests : IDisposable
{
    private readonly Faker _faker = new();
    private readonly SqliteConnection _keepAliveConnection;
    private readonly ProductInventoryReadRepository _products;

    public ProductInventoryReadRepositoryTests()
    {
        var dbName = $"TestDb_{Guid.NewGuid():N}";
        var connectionString = $"Data Source={dbName};Mode=Memory;Cache=Shared";

        _keepAliveConnection = new SqliteConnection(connectionString);
        _keepAliveConnection.Open();
        RegisterCustomFunctions(_keepAliveConnection);

        CreateTables(_keepAliveConnection);

        var connectionFactory = new TestDbConnectionFactory(() =>
        {
            var connection = new SqliteConnection(connectionString);
            RegisterCustomFunctions(connection);
            return connection;
        });

        _products = new ProductInventoryReadRepository(connectionFactory);
    }

    public void Dispose()
    {
        _keepAliveConnection.Dispose();
    }

    [Fact]
    public async Task GetAsync_WhenProductsExist_ShouldReturnPagedResults()
    {
        // Arrange
        var count = 5;
        for (var i = 0; i < count; i++)
        {
            InsertProductWithInventory(
                _faker.Commerce.ProductName(),
                _faker.Commerce.Ean13(),
                _faker.Commerce.ProductDescription(),
                _faker.Finance.Amount(10, 100),
                quantity: _faker.Random.Int(1, 50));
        }

        // Act
        var result = await _products.GetAsync(pageNumber: 1, pageSize: 10, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Metadata.TotalItems);
        Assert.Equal(count, result.Result.Count);
        Assert.Equal(1, result.Metadata.PageNumber);
        Assert.Equal(10, result.Metadata.PageSize);
        Assert.Equal(1, result.Metadata.TotalPages);
    }

    [Fact]
    public async Task GetAsync_WhenCancellationRequested_ShouldThrowOperationCanceledException()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _products.GetAsync(1, 10, cts.Token));
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProductInventoryReadModel()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        var sku = _faker.Commerce.Ean13();
        var desc = _faker.Commerce.ProductDescription();
        var price = _faker.Finance.Amount(10, 100);
        var quantity = _faker.Random.Int(5, 50);

        var productId = InsertProductWithInventory(name, sku, desc, price, quantity);

        // Act
        var result = await _products.GetByIdAsync(productId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(name, result.Name);
        Assert.Equal(sku, result.Sku);
        Assert.Equal(desc, result.Description);
        Assert.Equal(price, result.Price);
        Assert.Equal(quantity, result.QuantityInStock);
        Assert.True(result.IsInventoryTracked);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var nonExistentId = _faker.Random.Long(9999, 99999);

        // Act
        var result = await _products.GetByIdAsync(nonExistentId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByStockLevelAsync_WhenProductsMatchStockCriteria_ShouldReturnFilteredResults()
    {
        // Arrange
        // var lowStockProduct = InsertProductWithInventory(
        //     _faker.Commerce.ProductName(),
        //     _faker.Commerce.Ean13(),
        //     _faker.Commerce.ProductDescription(),
        //     50m,
        //     quantity: 5);

        var midStockProduct = InsertProductWithInventory(
            _faker.Commerce.ProductName(),
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            50m,
            quantity: 25);

        // var highStockProduct = InsertProductWithInventory(
        //     _faker.Commerce.ProductName(),
        //     _faker.Commerce.Ean13(),
        //     _faker.Commerce.ProductDescription(),
        //     50m,
        //     quantity: 100);

        // Act
        var result = await _products.GetByStockLevelAsync(
            minStock: 20,
            maxStock: 50,
            pageNumber: 1,
            pageSize: 10,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Metadata.TotalItems);
        Assert.Single(result.Result);
        Assert.Equal(midStockProduct, result.Result.First().ProductId);
    }

    [Fact]
    public async Task SearchByNameAsync_WhenProductsMatchName_ShouldReturnMatchingResults()
    {
        // Arrange
        var uniqueKeyword = $"Unique_{_faker.Random.AlphaNumeric(6)}";
        var matchingName = $"Zeiss {uniqueKeyword} Microscope";
        var otherName = "Unrelated Binoculars";

        var matchingProductId = InsertProductWithInventory(
            matchingName,
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            100m,
            quantity: 10);

        InsertProductWithInventory(
            otherName,
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            100m,
            quantity: 10);

        // Act
        var result = await _products.SearchByNameAsync(
            uniqueKeyword,
            pageNumber: 1,
            pageSize: 10,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Metadata.TotalItems);
        Assert.Single(result.Result);
        Assert.Equal(matchingProductId, result.Result.First().ProductId);
        Assert.Equal(matchingName, result.Result.First().Name);
    }

    [Fact]
    public async Task SearchByNameAsync_WhenNoProductsMatch_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        InsertProductWithInventory(
            "Zeiss Prism",
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            100m,
            quantity: 10);

        // Act
        var result = await _products.SearchByNameAsync(
            "NonExistentTextToSearch12345",
            pageNumber: 1,
            pageSize: 10,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Metadata.TotalItems);
        Assert.Empty(result.Result);
    }

    private static void RegisterCustomFunctions(SqliteConnection connection)
    {
        connection.CreateFunction("GREATEST", (string? a, string? b) =>
        {
            if (a is null && b is null) return null;
            if (a is null) return b;
            if (b is null) return a;
            return string.Compare(a, b, StringComparison.Ordinal) >= 0 ? a : b;
        });
    }

    private static void CreateTables(SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS "Products" (
                "Id" INTEGER PRIMARY KEY AUTOINCREMENT,
                "Name" TEXT NOT NULL,
                "Sku" TEXT NOT NULL,
                "Description" TEXT,
                "Price" TEXT NOT NULL,
                "IsActive" INTEGER NOT NULL,
                "IsDeleted" INTEGER NOT NULL,
                "CreatedAt" TEXT NOT NULL,
                "UpdatedAt" TEXT,
                "DeletedAt" TEXT
            );

            CREATE TABLE IF NOT EXISTS "Inventory" (
                "Id" INTEGER PRIMARY KEY AUTOINCREMENT,
                "ProductId" INTEGER NOT NULL,
                "Quantity" INTEGER,
                "CreatedAt" TEXT NOT NULL,
                "UpdatedAt" TEXT
            );
            """;

        connection.Execute(sql);
    }

    private long InsertProductWithInventory(
        string name,
        string sku,
        string? description,
        decimal price,
        int? quantity)
    {
        var now = DateTime.UtcNow.ToString("O");
        const string insertProductSql = """
            INSERT INTO "Products" ("Name", "Sku", "Description", "Price", "IsActive", "IsDeleted", "CreatedAt")
            VALUES (@name, @sku, @description, @price, 1, 0, @createdAt);
            SELECT last_insert_rowid();
            """;

        var productId = _keepAliveConnection.ExecuteScalar<long>(insertProductSql, new
        {
            name,
            sku,
            description,
            price = price.ToString(CultureInfo.InvariantCulture),
            createdAt = now
        });

        if (quantity.HasValue)
        {
            const string insertInventorySql = """
                INSERT INTO "Inventory" ("ProductId", "Quantity", "CreatedAt", "UpdatedAt")
                VALUES (@productId, @quantity, @createdAt, @updatedAt);
                """;

            _keepAliveConnection.Execute(insertInventorySql, new
            {
                productId,
                quantity = quantity.Value,
                createdAt = now,
                updatedAt = now
            });
        }

        return productId;
    }

    private sealed class TestDbConnectionFactory(Func<IDbConnection> connectionProvider) : IDbConnectionFactory
    {
        public IDbConnection Create() => connectionProvider();
    }
}
