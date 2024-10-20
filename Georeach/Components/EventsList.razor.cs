using Microsoft.AspNetCore.Components;

namespace Georeach.Components;

public class EventsListBase : ComponentBase
{

    protected record Event(string Title, DateTime Date, string Description, string Location);

    protected List<Event> events { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1000);

        events = new List<Event>
{
        new Event(
                    "Art Exhibition",
                    DateTime.Parse("2023-11-10"),
                    "A display of modern art.",
                    "New York, NY"),
        new Event(
            "Food Festival",
        DateTime.Parse("2023-11-15"),
        "A day of gourmet food.",
        "San Francisco, CA"),
        new Event(
            "Tech Conference",
        DateTime.Parse("2023-11-20"),
        "Exploring the latest in tech.",
        "Austin, TX"
        )
};
    }
}