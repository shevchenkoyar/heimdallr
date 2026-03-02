using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    public DbSet<UserIpRule> UserIpRules => Set<UserIpRule>();
    
    public DbSet<Meter> Meters => Set<Meter>();
    
    public DbSet<MeterEndpoint> MeterEndpoints => Set<MeterEndpoint>();
    
    public DbSet<ProxyPort> ProxyPorts => Set<ProxyPort>();
    
    public DbSet<ProxySession> ProxySessions => Set<ProxySession>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Heimdallr);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
