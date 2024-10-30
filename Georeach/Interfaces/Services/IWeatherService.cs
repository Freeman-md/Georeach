using System;
using Georeach.Models;

namespace Georeach.Interfaces.Services;

public interface IWeatherService
{
    public Task<Weather?> GetWeather(string location);
}
