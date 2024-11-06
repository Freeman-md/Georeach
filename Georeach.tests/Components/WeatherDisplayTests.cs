using System;
using Georeach.Components;

namespace Georeach.tests.Components;

public class WeatherDisplayTests : TestContext
{
    [Fact]
    public void WeatherDisplay_ShouldRenderData_WhenWeatherIsAvailable() {
        #region Arrange
            var component = RenderComponent<WeatherDisplay>();
        #endregion

        #region Act
            component.WaitForState(() => component.Instance.WeatherData != null);

            var weatherData = component.Instance.WeatherData;
        #endregion

        #region Assert
            Assert.NotNull(weatherData);

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
    public void WeatherDisplay_ShouldLoadingIndicator_WhenDataIsFetching() {
        #region Arrange
            var component = RenderComponent<WeatherDisplay>();
        #endregion

        #region Act
            component.Instance.WeatherData = null;

            var weatherData = component.Instance.WeatherData;
        #endregion

        #region Assert
            Assert.Null(weatherData);

            var loadingElement = component.Find("dotlottie-player#loading");

            Assert.NotNull(loadingElement);

            var idValue = loadingElement.GetAttribute("id");
            Assert.Equal("loading", idValue);
        #endregion
    }
}
