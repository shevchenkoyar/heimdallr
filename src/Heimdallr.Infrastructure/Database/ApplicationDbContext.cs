using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Domain.Entities;
using Heimdallr.Infrastructure.Database.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Database;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IDbContext
{
    public DbSet<User> DomainUsers => Set<User>();

    public DbSet<UserIpRule> UserIpRules => Set<UserIpRule>();
    
    public DbSet<Meter> Meters => Set<Meter>();
    
    public DbSet<MeterEndpoint> MeterEndpoints => Set<MeterEndpoint>();
    
    public DbSet<ProxyPort> ProxyPorts => Set<ProxyPort>();
    
    public DbSet<ProxySession> ProxySessions => Set<ProxySession>();

    async Task IDbContext.SaveChangesAsync(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema(Schemas.Heimdallr);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
