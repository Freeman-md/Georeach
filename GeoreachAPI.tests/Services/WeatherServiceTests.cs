using System.Net;
using System.Text.Json;
using GeoreachAPI.Models;
using GeoreachAPI.Services;
using GeoreachAPI.tests.Builders;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

namespace GeoreachAPI.tests.Services;

public class WeatherServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly IConfiguration _configuration;

    public WeatherServiceTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var inMemorySettings = new Dictionary<string, string>{
            { "WeatherApi:ApiKey", "test-api-key" },
            { "WeatherApi:BaseUrl", "https://api.weather.com" },
        };

        _configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(inMemorySettings)
                            .Build();
    }

    private HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string responseBody)
    {
        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseBody)
            });

        return new HttpClient(messageHandlerMock.Object)
        {
            BaseAddress = new Uri(_configuration["WeatherApi:BaseUrl"])
        };
    }

    private WeatherService CreateWeatherService(string jsonResponse)
    {
        var client = CreateMockHttpClient(HttpStatusCode.OK, jsonResponse);

        return new WeatherService(client, _configuration);
    }

    [Fact]
    public async Task GetWeather_ShouldReturnWeatherData_WhenRequestIsSuccessful()
    {
        #region Arrange
        Weather expectedWeather = new WeatherBuilder().Build();
        var jsonResponse = JsonSerializer.Serialize(expectedWeather);

        WeatherService weatherService = CreateWeatherService(jsonResponse);
        #endregion

        #region Act
        var result = await weatherService.GetWeather("New York");
        #endregion

        #region Assert
        Assert.NotNull(result);
        Assert.Equal(expectedWeather.Current.Temp_c, result.Current.Temp_c);
        Assert.Equal(expectedWeather.Location.Country, result.Location.Country);
        #endregion
    }

    [Fact]
    public async Task GetWeather_ShouldThrowArgumentException_WhenLocationIsNullOrEmpty()
    {
        #region Arrange
        var weatherService = CreateWeatherService("{}");
        #endregion

        #region Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => weatherService.GetWeather(""));
        await Assert.ThrowsAsync<ArgumentException>(() => weatherService.GetWeather(null!));
        #endregion
    }
}
