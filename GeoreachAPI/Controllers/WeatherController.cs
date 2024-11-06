using GeoreachAPI.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoreachAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest("Location query parameter is required.");
            }
            
            var weatherData = await _weatherService.GetWeather(location);
            if (weatherData == null)
            {
                return NotFound("Weather data not available for the specified location.");
            }

            return Ok(weatherData);
        }
    }
}
