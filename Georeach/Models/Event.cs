using System;

namespace Georeach.Models;

public record EventDate(string localDate, bool dateTBD, bool dateTBA, bool timeTBA, bool noSpecificTime);

public record EventDates(EventDate start, EventDate end, string timezone);

public record EventImage(string ratio, string url, int width, int height, bool fallback);

public record Event(string Id, string Name, string Type, string Url, EventDates dates, EventImage[] images);
