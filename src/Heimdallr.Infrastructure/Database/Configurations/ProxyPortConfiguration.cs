using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdallr.Infrastructure.Database.Configurations;


public sealed class ProxyPortConfiguration : IEntityTypeConfiguration<ProxyPort>
{
    public void Configure(EntityTypeBuilder<ProxyPort> builder)
    {
        builder.ToTable("proxy_ports");
        
        builder.HasKey(x => x.Port);

        builder.Property(x => x.State)
            .IsRequired();

        builder.Property(x => x.ReservedAt);

        builder.Property(x => x.LastUsedAt);
        
        builder.Ignore(x => x.CurrentSession);
    }
}
