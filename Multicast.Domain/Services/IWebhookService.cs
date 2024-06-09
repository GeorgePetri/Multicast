using Multicast.Domain.Models;

namespace Multicast.Domain.Services
{
    public interface IWebhookService
    {
        Task SubscribeAsync(Subscription subscription);
    }
}