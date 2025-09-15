using System.Net;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Web.Test;

public class ApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestingAPositive()
    {
        // Arrange - the setup
        // client created
        
        // Act - the execution
        var response = await _client.GetAsync("/");

        // Assert - compare and validate
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Hello World!");
    }

    [Fact]
    public async Task TestingANegative()
    {
        // Arrange - the setup
        // client created

        // Act - the execution
        var response = await _client.GetAsync("/");

        // Assert - compare and validate
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
    }
}
