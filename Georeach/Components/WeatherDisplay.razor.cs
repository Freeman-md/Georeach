using Microsoft.AspNetCore.Components;

namespace Georeach.Components;

public record WeatherData(int temperature, int humidity, int windSpeed);

public partial class WeatherDisplay : ComponentBase {
    public WeatherData? weatherData { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(500);

        weatherData = new WeatherData(
            70,
            90,
            49
        );
    }
}