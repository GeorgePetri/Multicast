using Multicast.Domain.Models;

namespace Multicast.Domain.Services
{
    public interface IWebHookService
    {
        Task SubscribeAsync(Subscription subscription);
    }
}