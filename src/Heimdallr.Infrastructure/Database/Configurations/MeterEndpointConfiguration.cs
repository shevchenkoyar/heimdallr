using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdallr.Infrastructure.Database.Configurations;

public sealed class MeterEndpointConfiguration : IEntityTypeConfiguration<MeterEndpoint>
{
    public void Configure(EntityTypeBuilder<MeterEndpoint> builder)
    {
        builder.ToTable("meter_endpoints");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TransportType)
            .IsRequired();

        builder.Property(x => x.Host)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Port);

        builder.Property(x => x.IsEnabled)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new { x.MeterId, x.IsEnabled });
    }
}
