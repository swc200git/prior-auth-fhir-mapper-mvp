using Api.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using System;
using System.Threading.Tasks;


namespace Api.IntegrationTests;
public class PriorAuthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PriorAuthEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ValidRequest_ReturnsCreated()
    {
        var req = new PriorAuthRequest(
            PatientId: "pat01",
            MemberId: "mem01",
            ProviderNpi: "1234567890",
            ServiceCode: "S1234",
            DiagnosisCode: "Z01.1",
            ServiceDate: DateOnly.Parse("2025-09-24"),
            PlaceOfService: "11",
            Priority: "normal"
        );

        var response = await _client.PostAsJsonAsync("/prior-auth", req);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<CreatedResponse>();
        Assert.NotNull(result);
        Assert.Equal("Submitted", result.Status);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task MissingRequiredFields_ReturnsBadRequest()
    {
        var req = new PriorAuthRequest(
            PatientId: "",
            MemberId: "mem01",
            ProviderNpi: "",
            ServiceCode: "",
            DiagnosisCode: "",
            ServiceDate: DateOnly.Parse("2025-09-24"),
            PlaceOfService: "",
            Priority: ""
        );

        var response = await _client.PostAsJsonAsync("/prior-auth", req);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var message = await response.Content.ReadAsStringAsync();
        Assert.Contains("required", message);
    }
}
