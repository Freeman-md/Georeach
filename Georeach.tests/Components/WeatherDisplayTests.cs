using System;
using System.Threading.Tasks;
using Georeach.Components;
using Georeach.Interfaces.Services;
using Georeach.Models;
using Georeach.tests.Builders;
using Moq;

namespace Georeach.tests.Components;

public class WeatherDisplayTests : TestContext
{
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly Weather _mockWeatherData;

    public WeatherDisplayTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();

        // Mock weather data
        _mockWeatherData = new WeatherBuilder().Build();

        // Register the mock service
        Services.AddSingleton<IWeatherService>(_weatherServiceMock.Object);
    }

    [Fact]
    public void WeatherDisplay_ShouldRenderData_WhenWeatherIsAvailable()
    {
        #region Arrange
        _weatherServiceMock.Setup(s => s.GetWeather(It.IsAny<string>())).ReturnsAsync(_mockWeatherData);
        var component = RenderComponent<WeatherDisplay>(parameters => parameters.Add(p => p.Location, "Paris"));
        #endregion

        #region Act
        component.WaitForState(() => component.Instance.WeatherData != null);
        #endregion

        #region Assert
        var temperatureContainer = component.Find("div#temperature-container");
        var humidityContainer = component.Find("div#humidity-container");
        var windSpeedContainer = component.Find("div#wind-speed-container");

        var temperatureElement = temperatureContainer.QuerySelector("#temperature-in-celsius");
        var humidityElement = humidityContainer.QuerySelector("#humidity-in-percentage");
        var windSpeedElement = windSpeedContainer.QuerySelector("#wind-speed-in-mph");

        Assert.NotNull(temperatureContainer);
        Assert.NotNull(humidityContainer);
        Assert.NotNull(windSpeedContainer);

        Assert.NotNull(temperatureElement);
        Assert.NotNull(humidityElement);
        Assert.NotNull(windSpeedElement);

        Assert.False(string.IsNullOrWhiteSpace(temperatureElement.TextContent.Trim()));
        Assert.False(string.IsNullOrWhiteSpace(humidityElement.TextContent.Trim()));
        Assert.False(string.IsNullOrWhiteSpace(windSpeedElement.TextContent.Trim()));
        #endregion
    }

    [Fact]
    public void WeatherDisplay_ShouldShowLoadingIndicator_WhenDataIsFetching()
    {
        #region Arrange
        _weatherServiceMock.Setup(s => s.GetWeather(It.IsAny<string>())).ReturnsAsync((Weather)null);
        var component = RenderComponent<WeatherDisplay>(parameters => parameters.Add(p => p.Location, "Paris"));
        #endregion

        #region Act
        component.WaitForState(() => component.Instance.WeatherData == null);
        #endregion

        #region Assert
        var loadingElement = component.Find("dotlottie-player#loading");
        Assert.NotNull(loadingElement);
        Assert.Equal("loading", loadingElement.GetAttribute("id"));
        #endregion
    }
}
