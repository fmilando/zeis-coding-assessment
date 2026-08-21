using System.Net;
using System.Net.Http.Headers;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Products;

public class GetProductsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetProductsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        factory.ProductRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenNonHappyPath()
    {
        // Act
        var response = await _client.GetAsync("/api/products?page=0");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
