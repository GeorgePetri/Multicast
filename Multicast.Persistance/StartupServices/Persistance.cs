using Microsoft.Extensions.DependencyInjection;
using Multicast.Domain.Services;
using Multicast.Persistance.Services;

namespace Multicast.Persistance.StartupServices;

public static class Persistance
{
    public static void AddPersistance(this IServiceCollection services)
    {
        services.AddTransient<IWebhookService, WebhookService>();
    }
}
