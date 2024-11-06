using System;
using GeoreachAPI.Models;

namespace GeoreachAPI.Interfaces.Services;

public interface IWeatherService
{
    public Task<Weather?> GetWeather(string location);
}
