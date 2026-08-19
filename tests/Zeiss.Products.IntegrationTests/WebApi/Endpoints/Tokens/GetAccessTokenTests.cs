using System.Net;
using System.Net.Http.Json;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Tokens;

public class GetAccessTokenTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetAccessTokenTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenHappyPath()
    {
        // Arrange
        var request = new
        {
            SecretId = "ValidId", 
            SecretKey = "ValidKey"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
