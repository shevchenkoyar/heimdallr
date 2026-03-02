using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdallr.Infrastructure.Database.Configurations;

public sealed class ProxySessionConfiguration : IEntityTypeConfiguration<ProxySession>
{
    public void Configure(EntityTypeBuilder<ProxySession> builder)
    {
        builder.ToTable("proxy_sessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(x => x.ClientIpPolicy)
            .IsRequired();

        builder.Property(x => x.PinnedClientIp)
            .HasMaxLength(64);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.FaultReason)
            .HasMaxLength(2000);
        
        builder.HasOne(x => x.Port)
            .WithMany()
            .HasForeignKey(x => x.ProxyPort)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeterId);
        builder.HasIndex(x => x.Status);

        // 1 active session on meter (Starting/Active)
        builder.HasIndex(x => x.MeterId)
            .HasDatabaseName("ux_proxy_sessions_meter_active")
            .IsUnique()
            .HasFilter("status IN (1, 2)"); // Starting, Active

        // 1 active session on port (Starting/Active)
        builder.HasIndex(x => x.ProxyPort)
            .HasDatabaseName("ux_proxy_sessions_port_active")
            .IsUnique()
            .HasFilter("status IN (1, 2)"); // Starting, Active
        
        builder.HasIndex(x => new { x.UserId, x.Status });
    }
}
