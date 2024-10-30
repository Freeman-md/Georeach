using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Georeach.Models;
using Georeach.Services;
using Georeach.tests.Builders;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

namespace Georeach.tests.UnitTests;

public class WeatherServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly IConfiguration _configuration;

    public WeatherServiceTests() {
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

    [Fact]
    public async Task GetWeather_ShouldReturnWeatherData_WhenRequestIsSuccessful() {
        #region Arrange
            Weather expectedWeather = new WeatherBuilder().Build();
            var jsonResponse = JsonSerializer.Serialize(expectedWeather);

            var client = CreateMockHttpClient(HttpStatusCode.OK, jsonResponse);
            _httpClientFactoryMock.Setup(httpClientFactoryMock => httpClientFactoryMock.CreateClient("WeatherApiClient")).Returns(client);

            WeatherService weatherService = new WeatherService(_httpClientFactoryMock.Object);
        #endregion

        #region Act
            var result = await weatherService.GetWeather("New York");
        #endregion

        #region Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWeather.CurrentWeather.Temp_c, result.CurrentWeather.Temp_c);
            Assert.Equal(expectedWeather.Location.Country, result.Location.Country);
        #endregion
    }
}
