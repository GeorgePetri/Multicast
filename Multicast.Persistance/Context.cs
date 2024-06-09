using Microsoft.EntityFrameworkCore;
using Multicast.Persistance.Entities;

namespace Multicast.Persistance;

public class Context : DbContext
{
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations from the assembly that contains the SubscriptionEntity, scales better than adding each configuration in OnModelCreating
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionEntity).Assembly);
    }

}
