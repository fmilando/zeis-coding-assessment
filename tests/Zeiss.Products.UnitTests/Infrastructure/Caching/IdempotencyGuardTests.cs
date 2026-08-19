using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StackExchange.Redis;
using Zeiss.Products.Infrastructure.Caching;

namespace Zeiss.Products.UnitTests.Infrastructure.Caching;

public sealed class IdempotencyGuardTests
{
    private readonly Faker _faker = new();
    private readonly Mock<IConnectionMultiplexer> _redisMock = new();
    private readonly Mock<IDatabase> _databaseMock = new();
    private readonly Mock<ILogger<IdempotencyGuard>> _loggerMock = new();
    private readonly RedisSettings _settings;
    private readonly IdempotencyGuard _guard;

    public IdempotencyGuardTests()
    {
        _settings = new RedisSettings
        {
            ConnectionString = _faker.Internet.Url(),
            RecordRetentionInSeconds = _faker.Random.Int(30, 300)
        };

        _redisMock.Setup(redis =>
            redis.GetDatabase(It.IsAny<int>(), It.IsAny<object>())
        ).Returns(_databaseMock.Object);

        var options = Options.Create(_settings);
        _guard = new IdempotencyGuard(_redisMock.Object, options, _loggerMock.Object);
    }

    [Fact]
    public async Task GetValueAsync_WhenKeyExists_ShouldSuccess()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        var expectedValue = _faker.Lorem.Sentence();

        _databaseMock.Setup(cache =>
            cache.StringGetAsync(It.Is<RedisKey>(k => k == key), CommandFlags.None)
        ).ReturnsAsync(expectedValue);

        // Act
        var result = await _guard.GetValueAsync(key, CancellationToken.None);

        // Assert
        Assert.Equal(expectedValue, result);
        _databaseMock.Verify(db => db.StringGetAsync(It.Is<RedisKey>(k => k == key), CommandFlags.None), Times.Once);
    }

    [Fact]
    public async Task GetValueAsync_WhenKeyDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);

        _databaseMock.Setup(cache =>
            cache.StringGetAsync(It.Is<RedisKey>(k => k == key), CommandFlags.None)
        ).ReturnsAsync(RedisValue.Null);

        // Act
        var result = await _guard.GetValueAsync(key, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetValueAsync_WhenCancellationRequested_ShouldThrowException()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);

        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _guard.GetValueAsync(key, cancellationTokenSource.Token)
        );
    }

    [Fact]
    public async Task SetValueAsync_WhenValidKeyAndValue_ShouldSetCacheWithTimeout()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        var value = _faker.Lorem.Sentence();
        var timeout = TimeSpan.FromMinutes(_faker.Random.Int(1, 10));

        // Act
        await _guard.SetValueAsync(key, value, timeout, CancellationToken.None);

        // Assert
        var invocation = _databaseMock.Invocations.FirstOrDefault(call =>
            call.Method.Name == nameof(IDatabase.StringSetAsync)
        );

        Assert.NotNull(invocation);
        Assert.Equal(key, invocation.Arguments[0].ToString());
        Assert.Equal(value, invocation.Arguments[1].ToString());
    }

    [Fact]
    public async Task SetValueAsync_WhenCancellationRequested_ShouldThrowOperationCanceledException()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        var value = _faker.Lorem.Sentence();
        var timeout = TimeSpan.FromSeconds(60);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _guard.SetValueAsync(key, value, timeout, cts.Token));
    }

    [Fact]
    public async Task TryLockAsync_WhenLockAcquired_ShouldReturnTrueAndLockId()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        _databaseMock
            .Setup(db => db.LockTakeAsync(
                It.Is<RedisKey>(k => k == key),
                It.IsAny<RedisValue>(),
                TimeSpan.FromSeconds(_settings.RecordRetentionInSeconds),
                CommandFlags.None))
            .ReturnsAsync(true);

        // Act
        var (success, lockId) = await _guard.TryLockAsync(key, CancellationToken.None);

        // Assert
        Assert.True(success);
        Assert.NotNull(lockId);
        Assert.NotEqual(Guid.Empty, lockId.Value);
    }

    [Fact]
    public async Task TryLockAsync_WhenLockFails_ShouldReturnFalseAndNullLockIdAndLogInformation()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        _databaseMock
            .Setup(db => db.LockTakeAsync(
                It.Is<RedisKey>(k => k == key),
                It.IsAny<RedisValue>(),
                TimeSpan.FromSeconds(_settings.RecordRetentionInSeconds),
                CommandFlags.None))
            .ReturnsAsync(false);

        // Act
        var (success, lockId) = await _guard.TryLockAsync(key, CancellationToken.None);

        // Assert
        Assert.False(success);
        Assert.Null(lockId);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to lock")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task TryLockAsync_WhenCancellationRequested_ShouldThrowOperationCanceledException()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _guard.TryLockAsync(key, cts.Token));
    }

    [Fact]
    public async Task UnlockAsync_WhenLockExists_ShouldReleaseLockWithFireAndForget()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        var lockId = Guid.NewGuid();

        _databaseMock
            .Setup(db => db.LockReleaseAsync(
                It.Is<RedisKey>(k => k == key),
                It.Is<RedisValue>(v => v == lockId.ToString()),
                CommandFlags.FireAndForget))
            .ReturnsAsync(true);

        // Act
        await _guard.UnlockAsync(key, lockId, CancellationToken.None);

        // Assert
        _databaseMock.Verify(db => db.LockReleaseAsync(
            It.Is<RedisKey>(k => k == key),
            It.Is<RedisValue>(v => v == lockId.ToString()),
            CommandFlags.FireAndForget), Times.Once);
    }

    [Fact]
    public async Task UnlockAsync_WhenCancellationRequested_ShouldThrowOperationCanceledException()
    {
        // Arrange
        var key = _faker.Random.AlphaNumeric(10);
        var lockId = Guid.NewGuid();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _guard.UnlockAsync(key, lockId, cts.Token));
    }
}
