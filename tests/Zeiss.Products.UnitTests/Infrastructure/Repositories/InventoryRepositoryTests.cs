using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Infrastructure.Database;
using Zeiss.Products.Infrastructure.Repositories;

namespace Zeiss.Products.UnitTests.Infrastructure.Repositories;

public sealed class InventoryRepositoryTests : IDisposable
{
    private readonly Faker _faker = new();
    private readonly SqliteConnection _connection;
    private readonly PersistenceDbContext _dbContext;
    private readonly InventoryRepository _inventory;

    public InventoryRepositoryTests()
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

        _inventory = new InventoryRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public async Task AddAsync_WhenValidInventory_ShouldPersistAndReturnInventory()
    {
        // Arrange
        var productId = _faker.Random.Long(1, 1000);
        var quantity = _faker.Random.Int(10, 500);
        var inventory = new Inventory(productId, quantity);

        // Act
        var result = await _inventory.AddAsync(inventory, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(quantity, result.Quantity);

        var saved = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.ProductId == productId);
        Assert.NotNull(saved);
        Assert.Equal(quantity, saved.Quantity);
    }

    [Fact]
    public async Task GetAsync_WhenInventoryExists_ShouldReturnInventory()
    {
        // Arrange
        var productId = _faker.Random.Long(1, 1000);
        var quantity = _faker.Random.Int(10, 500);
        var inventory = new Inventory(productId, quantity);
        await _inventory.AddAsync(inventory, CancellationToken.None);

        // Act
        var result = await _inventory.GetAsync(productId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(quantity, result.Quantity);
    }

    [Fact]
    public async Task GetAsync_WhenInventoryDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var nonExistentProductId = _faker.Random.Long(9999, 99999);

        // Act
        var result = await _inventory.GetAsync(nonExistentProductId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenInventoryExists_ShouldUpdateQuantity()
    {
        // Arrange
        var productId = _faker.Random.Long(1, 1000);
        var initialQuantity = _faker.Random.Int(10, 50);
        var added = await _inventory.AddAsync(new Inventory(productId, initialQuantity), CancellationToken.None);

        var updatedQuantity = _faker.Random.Int(60, 200);
        var inventoryToUpdate = new Inventory(
            added.Id,
            productId,
            updatedQuantity,
            added.CreatedAt,
            DateTime.UtcNow);

        // Act
        var result = await _inventory.UpdateAsync(inventoryToUpdate, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedQuantity, result.Quantity);

        _dbContext.ChangeTracker.Clear();

        var inDb = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == added.Id);
        Assert.NotNull(inDb);
        Assert.Equal(updatedQuantity, inDb.Quantity);
    }

    [Fact]
    public async Task Transaction_WhenStartedAndCompleted_ShouldCommitSuccessfully()
    {
        // Arrange
        var productId = _faker.Random.Long(1, 1000);
        var inventory = new Inventory(productId, 100);

        // Act
        await _inventory.StartAsync(CancellationToken.None);
        await _inventory.AddAsync(inventory, CancellationToken.None);
        await _inventory.CompleteAsync(CancellationToken.None);

        // Assert
        _dbContext.ChangeTracker.Clear();
        var inDb = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.ProductId == productId);

        Assert.NotNull(inDb);
        Assert.Equal(100, inDb.Quantity);
    }

    [Fact]
    public async Task Transaction_WhenDiscarded_ShouldRollbackSuccessfully()
    {
        // Arrange
        var productId = _faker.Random.Long(1, 1000);
        var inventory = new Inventory(productId, 100);

        // Act
        await _inventory.StartAsync(CancellationToken.None);
        await _inventory.AddAsync(inventory, CancellationToken.None);
        await _inventory.DiscardAsync(CancellationToken.None);

        // Assert
        _dbContext.ChangeTracker.Clear();

        var inDb = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.ProductId == productId);
        Assert.Null(inDb);
    }

    [Fact]
    public async Task StartAsync_WhenTransactionAlreadyStarted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        await _inventory.StartAsync(CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _inventory.StartAsync(CancellationToken.None));

        // Cleanup
        await _inventory.DiscardAsync(CancellationToken.None);
    }
}
