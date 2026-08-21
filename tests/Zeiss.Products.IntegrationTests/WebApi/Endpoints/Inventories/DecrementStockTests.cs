using System.Net;
using System.Net.Http.Headers;
using Moq;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Inventories;

public class DecrementStockTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DecrementStockTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        _factory.InventoryRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenInventoryDoesNotExist()
    {
        // Arrange
        _factory.InventoryRepositoryMock
            .Setup(x => x.GetByProductIdAsync(999000, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Inventory?)null);

        // Act
        var response = await _client.PostAsync("/api/products/99/decrement-stock/5", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
