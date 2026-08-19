using Bogus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Zeiss.Products.Domain.Entities;
using Zeiss.Products.Domain.Events;
using Zeiss.Products.Infrastructure.Messaging;

namespace Zeiss.Products.UnitTests.Infrastructure.Messaging;

public sealed class RabbitMqEventPublisherTests
{
    private readonly Faker _faker = new();
    private readonly Mock<IPublishEndpoint> _publishEndpointMock = new();
    private readonly Mock<ILogger<RabbitMqEventPublisher>> _loggerMock = new();
    private readonly RabbitMqEventPublisher _publisher;

    public RabbitMqEventPublisherTests()
    {
        _publisher = new RabbitMqEventPublisher(_publishEndpointMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task PublishAsync_WhenPublishSucceeds_ShouldCallEndpointAndLogInformation()
    {
        // Arrange
        var product = new Product(
            _faker.Commerce.ProductName(),
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            _faker.Finance.Amount(1, 500));
        var @event = new ProductCreatedEvent(product);

        _publishEndpointMock
            .Setup(x => x.Publish(@event, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _publisher.PublishAsync(@event, CancellationToken.None);

        // Assert
        _publishEndpointMock.Verify(
            x => x.Publish(@event, It.IsAny<CancellationToken>()),
            Times.Once);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Published event")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task PublishAsync_WhenPublishFails_ShouldCatchExceptionAndLogError()
    {
        // Arrange
        var product = new Product(
            _faker.Commerce.ProductName(),
            _faker.Commerce.Ean13(),
            _faker.Commerce.ProductDescription(),
            _faker.Finance.Amount(1, 500));
        var @event = new ProductCreatedEvent(product);
        var expectedException = new InvalidOperationException("RabbitMQ connection lost");

        _publishEndpointMock
            .Setup(x => x.Publish(@event, It.IsAny<CancellationToken>()))
            .ThrowsAsync(expectedException);

        // Act
        await _publisher.PublishAsync(@event, CancellationToken.None);

        // Assert
        _publishEndpointMock.Verify(
            x => x.Publish(@event, It.IsAny<CancellationToken>()),
            Times.Once);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to publish event")),
                expectedException,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
