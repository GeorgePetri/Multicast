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
        modelBuilder.Entity<SubscriptionEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Url).IsRequired();
            entity.HasIndex(e => e.Url).IsUnique();
        });
    }

}
