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

    public async Task<Subscription?> GetAsync(string url)
    {
        var result = await _context.Subscriptions
            .Where(s => s.Url == url)
            .FirstOrDefaultAsync();

        return result is null ? null : new Subscription(result.Url);
    }

    public async Task<Subscription[]> GetAllAsync()
    {
        var result = await _context.Subscriptions
            .Select(s => new Subscription(s.Url))
            .ToArrayAsync();

        return result;
    }

    public async Task SubscribeAsync(Subscription subscription)
    {
        var found = await GetAsync(subscription.Url);

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