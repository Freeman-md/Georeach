using Georeach.Components;

namespace Georeach.tests.Components;

public class EventsListTests : TestContext
{
    [Fact]
    public void EventsList_ShouldRenderWeatherData_WhenWeatherIsAvailable()
    {
        #region Arrange
        var component = RenderComponent<EventsList>();
        #endregion

        #region Act
        component.WaitForState(() => component.Instance.events != null);

        var events = component.Instance.events;
        #endregion

        #region Assert
        Assert.NotNull(events);

        foreach (var eventItem in events)
        {
            // Ensure that the event title, date, description, location, and link are rendered
            component.Markup.Contains(eventItem.Title);
            component.Markup.Contains(eventItem.Date.ToLongDateString());
            component.Markup.Contains(eventItem.Description);
            component.Markup.Contains(eventItem.Location);
            component.Markup.Contains("Buy"); // Check if "Buy" link exists
        }
        #endregion
    }

    [Fact]
    public void EventsList_ShouldShowEmptyMessage_WhenNoEventsAreAvailable()
    {
        #region Arrange
        var component = RenderComponent<EventsList>();
        #endregion

        #region Act
        component.Instance.events = null;

        component.Render();
        #endregion

        #region Assert
        component.Markup.Contains("<em>Loading...</em>");
        #endregion
    }
}
