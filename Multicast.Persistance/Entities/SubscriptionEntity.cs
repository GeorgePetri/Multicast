namespace Multicast.Persistance.Entities;

public class SubscriptionEntity
{
    public Guid Id { get; set; }
    public required string Url { get; set; }
}