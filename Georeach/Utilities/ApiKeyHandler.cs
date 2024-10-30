using System;

namespace Georeach.Utilities;

public class ApiKeyHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;
    private readonly string _clientName;

    public ApiKeyHandler(IConfiguration configuration, string clientName)
    {
        _configuration = configuration;
        _clientName = clientName;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Retrieve the API key from configuration for the current client
        var apiKey = _configuration[$"{_clientName}:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException($"API key not configured for client {_clientName}.");
        }

        // Append the API key as a query parameter
        var uriBuilder = new UriBuilder(request.RequestUri);
        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
        query["apiKey"] = apiKey;
        uriBuilder.Query = query.ToString();

        request.RequestUri = uriBuilder.Uri;

        return await base.SendAsync(request, cancellationToken);
    }
}

