using System;
using Georeach.Interfaces.Services;
using Georeach.Models;

namespace Georeach.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(IHttpClientFactory httpClientFactory) {
        _httpClient = httpClientFactory.CreateClient("WeatherApiClient");
    }

    public Task<Weather?> GetWeather(string location) {
        throw new NotImplementedException();
    }
}
