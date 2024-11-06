using System;
using System.Net.Http.Json;
using System.Text.Json;
using Georeach.Interfaces.Services;
using Georeach.Models;

namespace Georeach.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Weather?> GetWeather(string location)
    {
        return await _httpClient.GetFromJsonAsync<Weather>($"weather?location={location}");
    }
}
