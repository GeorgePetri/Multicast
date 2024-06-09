using Microsoft.EntityFrameworkCore;
using Multicast.Persistance.Entities;

namespace Multicast.Persistance;

public class Context : DbContext
{
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }

    // The default constructor is required by the EF Core for migrations
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "multicast.db");
        options.UseSqlite($"Data Source={dbPath}");
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
