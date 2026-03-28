using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdallr.Infrastructure.Database.Configurations;

internal sealed class UserIpRuleConfiguration : IEntityTypeConfiguration<UserIpRule>
{
    public void Configure(EntityTypeBuilder<UserIpRule> builder)
    {
        builder.ToTable("user_ip_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IpOrCidr)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.IsEnabled)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(500);

        builder.HasIndex(x => new { x.UserId, x.IsEnabled });
        
        builder.HasIndex(x => new { x.UserId, x.IpOrCidr })
            .IsUnique();
    }
}
