using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Database.Repositories;

namespace Zeiss.Products.UnitTests.Infrastructure.Repositories;

public sealed class ProductRepositoryTests : IDisposable
{
    private readonly Faker _faker = new();
    private readonly SqliteConnection _connection;
    private readonly PersistenceDbContext _dbContext;
    private readonly ProductRepository _products;

    public ProductRepositoryTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PersistenceDbContext>()
            .UseSqlite(_connection)
            .Options;

        var loggerMock = new Mock<ILogger<DbErrorInterceptor>>();
        var interceptor = new DbErrorInterceptor(loggerMock.Object);

        _dbContext = new PersistenceDbContext(options, interceptor);
        _dbContext.Database.EnsureCreated();

        _products = new ProductRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public async Task AddAsync_WhenValidProduct_ShouldPersistAndReturnProduct()
    {
        // Arrange
        var product = CreateFakeProduct();

        // Act
        var result = await _products.AddAsync(product, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Sku, result.Sku);
        Assert.Equal(product.Price, result.Price);

        var saved = await _dbContext.Products.FindAsync(result.Id);

        Assert.NotNull(saved);
        Assert.Equal(product.Name, saved.Name);
        Assert.Equal(product.Sku, saved.Sku);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var product = CreateFakeProduct();
        var added = await _products.AddAsync(product, CancellationToken.None);

        // Act
        var result = await _products.GetAsync(added.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(added.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Sku, result.Sku);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var nonExistentId = _faker.Random.Int(100_000, 999_000);

        // Act
        var result = await _products.GetAsync(nonExistentId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBySkuAsync_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var product = CreateFakeProduct();
        var added = await _products.AddAsync(product, CancellationToken.None);

        // Act
        var result = await _products.GetBySkuAsync(product.Sku, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(added.Id, result.Id);
        Assert.Equal(product.Sku, result.Sku);
    }

    [Fact]
    public async Task GetBySkuAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var nonExistentSku = _faker.Commerce.Ean13();

        // Act
        var result = await _products.GetBySkuAsync(nonExistentSku, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenProductExists_ShouldUpdateProperties()
    {
        // Arrange
        var product = CreateFakeProduct();
        var added = await _products.AddAsync(product, CancellationToken.None);

        var updatedName = _faker.Commerce.ProductName();
        var updatedSku = _faker.Commerce.Ean13();
        var updatedPrice = _faker.Finance.Amount(50, 500);
        var updatedDescription = _faker.Commerce.ProductDescription();

        var productToUpdate = new Product(
            added.Id,
            updatedName,
            updatedSku,
            updatedDescription,
            updatedPrice,
            true,
            false,
            added.CreatedAt,
            DateTime.UtcNow,
            null,
            null);

        // Act
        var result = await _products.UpdateAsync(productToUpdate, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedName, result.Name);

        _dbContext.ChangeTracker.Clear();

        var inDb = await _dbContext.Products.FindAsync(added.Id);
        Assert.NotNull(inDb);
        Assert.Equal(updatedName, inDb.Name);
        Assert.Equal(updatedSku, inDb.Sku);
        Assert.Equal(updatedPrice, inDb.Price);
        Assert.Equal(updatedDescription, inDb.Description);
    }

    [Fact]
    public async Task DeleteAsync_WhenProductExists_ShouldMarkAsDeletedAndInactive()
    {
        // Arrange
        var product = CreateFakeProduct();
        var added = await _products.AddAsync(product, CancellationToken.None);

        var deletedAt = DateTime.UtcNow;
        var productToDelete = new Product(
            added.Id,
            added.Name,
            added.Sku,
            added.Description,
            added.Price,
            false,
            true,
            added.CreatedAt,
            deletedAt,
            deletedAt,
            null);

        // Act
        await _products.DeleteAsync(productToDelete, CancellationToken.None);

        // Assert
        _dbContext.ChangeTracker.Clear();

        var inDb = await _dbContext.Products.FindAsync(added.Id);
        Assert.NotNull(inDb);
        Assert.False(inDb.IsActive);
        Assert.True(inDb.IsDeleted);
        Assert.NotNull(inDb.DeletedAt);
    }

    private Product CreateFakeProduct(int id = 0)
    {
        return new Product(
            id,
            _faker.Commerce.ProductName(),
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            _faker.Finance.Amount(1, 1001),
            true,
            false,
            DateTime.UtcNow,
            null,
            null,
            null);
    }
}
