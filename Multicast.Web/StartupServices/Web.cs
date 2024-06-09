using Microsoft.Extensions.DependencyInjection;
using Multicast.Domain.Services;
using Multicast.Web.Clients;
using Multicast.Web.Controllers;

namespace Multicast.Web.StartupServices;

public static class Web
{
    public static void AddWeb(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(WebhookController).Assembly);

        services.AddHttpClient<IEventService, EventClient>();
    }
}