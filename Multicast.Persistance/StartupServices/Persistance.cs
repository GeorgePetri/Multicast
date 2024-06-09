using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Multicast.Domain.Services;
using Multicast.Persistance.Services;

namespace Multicast.Persistance.StartupServices;

public static class Persistance
{
    public static void AddPersistance(this IServiceCollection services)
    {
        services.AddTransient<ISubscriptionService, SubscriptionService>();

        // DbContextPool is more efficient than DbContext because it reuses the same instance of the DbContext
        services.AddDbContextPool<Context>(
            o => o.UseSqlite("Data Source=../multicast.db")
        );
    }
}
