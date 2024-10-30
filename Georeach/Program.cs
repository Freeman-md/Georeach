using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Georeach;
using Georeach.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<ApiKeyHandler>();


builder.Services.AddHttpClient("WeatherApiClient", client => {
    client.BaseAddress = new Uri(builder.Configuration["WeatherApi:BaseUrl"]);
}).ConfigurePrimaryHttpMessageHandler(provider => new ApiKeyHandler(provider.GetRequiredService<IConfiguration>(), "WeatherApi"));

builder.Services.AddHttpClient("EventApiClient", client => {
    client.BaseAddress = new Uri(builder.Configuration["EventApi:BaseUrl"]);
}).ConfigurePrimaryHttpMessageHandler(provider => new ApiKeyHandler(provider.GetRequiredService<IConfiguration>(), "EventApi"));

await builder.Build().RunAsync();
