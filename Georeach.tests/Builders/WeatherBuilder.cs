using System;
using Georeach.Models;

namespace Georeach.tests.Builders;

public class WeatherBuilder
{
    private Weather _weather;

    public WeatherBuilder() {
        Location location = new Location("London", "Greater London", "United Kingdom", 389.39, 2893.389, "jjs989sjk", "sjk89sui", DateTime.Now);
        CurrentWeather currentWeather = new CurrentWeather(DateTime.Now, 389.389, 3892.3892, 389.3, 389.39, 34903, "east", 398);

        // Default Weather Initialization
        _weather = new Weather(location, currentWeather);
    }

    public Weather Build() {
        return _weather;
    }
}
