using System;

namespace Georeach.Models;

public record Location(string Name, string Region, string Country, double Lat, double Lon, string Tz_id, string Localtime_epoch, DateTime Localtime);
