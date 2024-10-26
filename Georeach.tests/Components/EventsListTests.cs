using System.Collections.Generic;
using Georeach.Components;
using Georeach.Models;

namespace Georeach.tests.Components;

public class EventsListTests : TestContext
{
    // [Fact]
    // public void EventsList_ShouldRenderWeatherData_WhenWeatherIsAvailable()
    // {
    //     #region Arrange
    //     var component = RenderComponent<EventsList>();
    //     #endregion

    //     #region Act
    //     component.WaitForState(() => component.Instance.events != null);

    //     var events = component.Instance.events;
    //     #endregion

    //     #region Assert
    //     Assert.NotNull(events);

    //     foreach (var eventItem in events)
    //     {
    //         // Ensure that the event title, date, description, location, and link are rendered
    //         component.Markup.Contains(eventItem.Title);
    //         component.Markup.Contains(eventItem.Date.ToLongDateString());
    //         component.Markup.Contains(eventItem.Description);
    //         component.Markup.Contains(eventItem.Location);
    //         component.Markup.Contains("Buy"); // Check if "Buy" link exists
    //     }
    //     #endregion
    // }

    [Fact]
    public void EventsList_ShouldShowLoadingIndicator_WhenDataIsFetching()
    {
        #region Arrange
        var component = RenderComponent<EventsList>();
        #endregion

        #region Act
        component.Instance.events = null;

        component.Render();
        #endregion

        #region Assert
        var loadingElement = component.Find("dotlottie-player");

        Assert.NotNull(loadingElement);

        var idValue = loadingElement.GetAttribute("id");
        Assert.Equal("loading", idValue);
        #endregion
    }

    [Fact]
    public void EventsList_ShouldShowEmptyMessage_WhenNoEventsAvailable()
    {
        #region Arrange
        var component = RenderComponent<EventsList>();
        #endregion

        #region Act
        component.Instance.events = new List<Event>();

        component.Render();
        #endregion

        #region Assert
        var emptyElement = component.Find("dotlottie-player");

        Assert.NotNull(emptyElement);

        var idValue = emptyElement.GetAttribute("id");
        Assert.Equal("empty", idValue);
        #endregion
    }
}
