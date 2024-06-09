using Microsoft.EntityFrameworkCore;
using Multicast.Domain.Models;
using Multicast.Domain.Services;
using Multicast.Persistance.Entities;

namespace Multicast.Persistance.Services;

public class WebhookService : IWebhookService
{
    private readonly Context _context;

    public WebhookService(Context context) =>
        _context = context;

    public async Task SubscribeAsync(Subscription subscription)
    {
        var found = await _context.Subscriptions
            .Where(s => s.Url == subscription.Url)
            .FirstOrDefaultAsync();

        if (found is not null)
            return;

        _context.Subscriptions.Add(new SubscriptionEntity
        {
            Id = Guid.NewGuid(),
            Url = subscription.Url
        });

        await _context.SaveChangesAsync();
    }
}