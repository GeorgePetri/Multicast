using Multicast.Domain.Models;
using Multicast.Domain.Services;

namespace Multicast.Persistance.Services;

public class WebhookService : IWebhookService
{
    public Task SubscribeAsync(Subscription subscription)
    {
        //todo impl 
        return Task.CompletedTask;
    }
}