using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdallr.Infrastructure.Database.Configurations;

internal sealed class MeterConfiguration : IEntityTypeConfiguration<Meter>
{
    public void Configure(EntityTypeBuilder<Meter> builder)
    {
        builder.ToTable("meters");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(200);

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(200);

        builder.Property(x => x.IsEnabled)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasIndex(x => x.IsEnabled);
        builder.HasIndex(x => x.SerialNumber);

        builder.HasMany(x => x.Endpoints)
            .WithOne(x => x.Meter)
            .HasForeignKey(x => x.MeterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.Meter)
            .HasForeignKey(x => x.MeterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
