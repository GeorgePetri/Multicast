using Multicast.Domain.Models;

namespace Multicast.Domain.Services
{
    public interface IEventService
    {
        Task PublishToAllAsync(Event @event);
    }
}