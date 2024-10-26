using System;

namespace Georeach.Models;

public record Current(DateTime Last_updated, double Temp_c, double Temp_f, double Wind_mph, double Wind_kph, double Wind_degree, string Wind_dir, double humidity);

public record Weather(Location Location, Current Current);