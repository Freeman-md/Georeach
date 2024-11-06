using System;
using System.Net.Http.Json;
using System.Text.Json;
using GeoreachAPI.Interfaces.Services;
using GeoreachAPI.Models;

namespace GeoreachAPI.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["WeatherApi:BaseUrl"] ?? throw new ArgumentNullException("Weather API base URL is missing.");
        _apiKey = configuration["WeatherApi:ApiKey"] ?? throw new ArgumentNullException("Weather API key is missing.");
    }

    public async Task<Weather?> GetWeather(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            throw new ArgumentException("Location is required", nameof(location));
        }

        var requestUrl = $"{_baseUrl}/current.json?q={location}&key={_apiKey}";

        return await _httpClient.GetFromJsonAsync<Weather>(requestUrl);
    }
}
