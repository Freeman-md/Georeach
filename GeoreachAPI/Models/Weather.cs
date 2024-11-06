using System;

namespace GeoreachAPI.Models
{
    public record Location(
        string Name,
        string Region,
        string Country,
        double Lat,
        double Lon,
        string Tz_id,
        long Localtime_epoch,
        string Localtime
    );

    public record Condition(
        string Text,
        string Icon,
        int Code
    );

    public record CurrentWeather(
        long Last_updated_epoch,
        string Last_updated,
        double Temp_c,
        double Temp_f,
        int Is_day,
        Condition Condition,
        double Wind_mph,
        double Wind_kph,
        double Wind_degree,
        string Wind_dir,
        double Pressure_mb,
        double Pressure_in,
        double Precip_mm,
        double Precip_in,
        int Humidity,
        int Cloud,
        double Feelslike_c,
        double Feelslike_f,
        double Windchill_c,
        double Windchill_f,
        double Heatindex_c,
        double Heatindex_f,
        double Dewpoint_c,
        double Dewpoint_f,
        double Vis_km,
        double Vis_miles,
        double Uv,
        double Gust_mph,
        double Gust_kph
    );

    public record Weather(
        Location Location,
        CurrentWeather Current
    );
}
