using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Domain.Entities;
using Heimdallr.Infrastructure.Database.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Database;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IApplicationDbContext
{
    public DbSet<UserIpRule> UserIpRules => Set<UserIpRule>();
    
    public DbSet<Meter> Meters => Set<Meter>();
    
    public DbSet<MeterEndpoint> MeterEndpoints => Set<MeterEndpoint>();
    
    public DbSet<ProxyPort> ProxyPorts => Set<ProxyPort>();
    
    public DbSet<ProxySession> ProxySessions => Set<ProxySession>();

    async Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.HasDefaultSchema(Schemas.Heimdallr);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
