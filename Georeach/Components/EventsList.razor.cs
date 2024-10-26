using Georeach.Models;
using Microsoft.AspNetCore.Components;

namespace Georeach.Components;



public partial class EventsList : ComponentBase
{
    public List<Event>? events { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1000);

        events = new List<Event>
{
        
};
    }
}