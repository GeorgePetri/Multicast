using System.Runtime.CompilerServices;
using Multicast.Domain.Models;

namespace Multicast.Domain.Services
{
    public interface IWebhookService
    {
        Task<Subscription?> GetAsync(string url);

        Task SubscribeAsync(Subscription subscription);
    }
}