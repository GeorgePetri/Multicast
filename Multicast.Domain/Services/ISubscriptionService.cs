using Multicast.Domain.Models;

namespace Multicast.Domain.Services
{
    public interface ISubscriptionService
    {
        Task<Subscription?> GetAsync(string url);

        // If I had more time, I would have have used batches and have a limit on the number of subscriptions to return
        Task<Subscription[]> GetAllAsync();

        Task SubscribeAsync(Subscription subscription);
    }
}