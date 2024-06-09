using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multicast.Persistance.Entities;

public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Url).IsRequired();
        builder.HasIndex(e => e.Url).IsUnique();
    }
}