using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Multicast.Domain.Models;
using Multicast.Domain.Services;

namespace Multicast.Web.Clients;

public class EventClient : IEventService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IWebhookService _webhookService;
    private readonly ILogger<EventClient> _logger;

    public EventClient(
        HttpClient httpClient,
        IWebhookService webhookService,
        ILogger<EventClient> logger)
    {
        _httpClient = httpClient;
        _webhookService = webhookService;
        _logger = logger;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }


    public async Task PublishToAllAsync(Event @event)
    {
        var events = await _webhookService.GetAllAsync();

        foreach (var webhook in events)
        {
            await PublishToUrlAsync(webhook.Url, @event);
        }
    }

    private async Task PublishToUrlAsync(string url, Event @event)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, @event);
            _logger.LogInformation("Event published to {url} with status code {statusCode}", url, response.StatusCode);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to publish event");
        }
    }
}
