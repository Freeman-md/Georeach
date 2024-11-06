using System;
using GeoreachAPI.Models;

namespace GeoreachAPI.tests.Builders
{
    public class WeatherBuilder
    {
        private Weather _weather;

        public WeatherBuilder()
        {
            var location = new Location(
                "London",
                "Greater London",
                "United Kingdom",
                51.5074, // Latitude
                -0.1278, // Longitude
                "Europe/London",
                1730895851, // Localtime epoch
                "2024-11-06 13:24"
            );

            var condition = new Condition(
                "Sunny",
                "//cdn.weatherapi.com/weather/64x64/day/113.png",
                1000 // Condition code
            );

            var currentWeather = new CurrentWeather(
                1730895300, // Last_updated_epoch
                "2024-11-06 13:15",
                16.8,       // Temp_c
                62.3,       // Temp_f
                1,          // Is_day (1 = Day, 0 = Night)
                condition,
                3.4,        // Wind_mph
                5.4,        // Wind_kph
                360,        // Wind_degree
                "N",        // Wind_dir
                1029,       // Pressure_mb
                30.37,      // Pressure_in
                0,          // Precip_mm
                0,          // Precip_in
                66,         // Humidity
                3,          // Cloud
                16.8,       // Feelslike_c
                62.3,       // Feelslike_f
                16.8,       // Windchill_c
                62.3,       // Windchill_f
                16.8,       // Heatindex_c
                62.3,       // Heatindex_f
                10.5,       // Dewpoint_c
                50.8,       // Dewpoint_f
                10,         // Vis_km
                6,          // Vis_miles
                1.3,        // UV
                3.9,        // Gust_mph
                6.3         // Gust_kph
            );

            // Initialize Weather with Location and CurrentWeather
            _weather = new Weather(location, currentWeather);
        }

        public Weather Build()
        {
            return _weather;
        }
    }
}
