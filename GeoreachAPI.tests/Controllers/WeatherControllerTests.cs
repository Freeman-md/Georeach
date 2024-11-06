using System;
using GeoreachAPI.Controllers;
using GeoreachAPI.Interfaces.Services;
using GeoreachAPI.Models;
using GeoreachAPI.tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GeoreachAPI.tests.Controllers;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly WeatherController _controller;

    public WeatherControllerTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();

        _controller = new WeatherController(_weatherServiceMock.Object);
    }

    [Fact]
    public async Task Index_ReturnsOkResult_WithWeatherData()
    {
        #region Arrange
        var location = "New York";
        var expectedWeather = new WeatherBuilder().Build();
        _weatherServiceMock.Setup(s => s.GetWeather(location)).ReturnsAsync(expectedWeather);
        #endregion

        #region Act
        var result = await _controller.Index(location);
        #endregion

        #region Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedWeather, okResult.Value);
        #endregion
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenWeatherDataIsNull()
    {
        #region Arrange
        var location = "InvalidLocation";
        _weatherServiceMock.Setup(s => s.GetWeather(location)).ReturnsAsync((Weather)null);
        #endregion

        #region Act
        var result = await _controller.Index(location);
        #endregion

        #region Assert
        Assert.IsType<NotFoundObjectResult>(result);
        #endregion
    }

    [Fact]
    public async Task Index_ReturnsError_WhenNoLocationIsPassed()
    {
        #region Arrange
            var location = "";
        #endregion

        #region Act
            var result = await _controller.Index(location);
        #endregion

        #region Assert
            Assert.IsType<BadRequestObjectResult>(result);
        #endregion
    }
}
