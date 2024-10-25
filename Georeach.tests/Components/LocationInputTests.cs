using System;
using System.Threading.Tasks;
using Georeach.Components;
using Microsoft.AspNetCore.Components;

namespace Georeach.tests.Components;

public class LocationInputTests : TestContext
{
    [Fact]
    public void LocationInput_ShouldRenderWithInitialLocation()
    {
        // Arrange
        string initialLocation = "Test Value";

        // Act
        var cut = RenderComponent<LocationInput>(parameters =>
            parameters.Add(p => p.Location, initialLocation)
        );

        // Assert
        var input = cut.Find("input");
        Assert.Equal(initialLocation, input.GetAttribute("value"));
    }


    [Fact]
    public async Task LocationInput_ShouldCallEventHandler_OnUserInput()
    {
        #region Arrange
        string initialLocation = "";
        string newLocation = "New Location";
        string capturedLocation = "";

        var component = RenderComponent<LocationInput>(parameters =>
            {
                parameters.Add(p => p.Location, initialLocation);
                parameters.Add(p => p.LocationChanged, (string val) => 
                {
                    capturedLocation = val;
                });
            });
        #endregion

        #region Act
        var inputElement = component.Find("input");
        await inputElement.InputAsync(new ChangeEventArgs()
        {
            Value = newLocation
        });

        Assert.Equal(newLocation, capturedLocation);
        #endregion
    }

    [Fact]
    public async Task LocationInput_ShouldNotTriggerEvent_WhenLocationUnchanged() {
        #region Arrange
        string initialLocation = "Location";
        int eventCallCount = 0;

        var component = RenderComponent<LocationInput>(
            parameters => {
                parameters.Add(p => p.Location, initialLocation);
                parameters.Add(p => p.LocationChanged, (string location) => {
                    eventCallCount++;
                });
            }
        );

        #endregion

        #region Act
            var inputElement = component.Find("input");
            await inputElement.InputAsync(new ChangeEventArgs() {
                Value = initialLocation
            });
        #endregion

        #region Assert
            Assert.Equal(0, eventCallCount);
        #endregion


    }
}
