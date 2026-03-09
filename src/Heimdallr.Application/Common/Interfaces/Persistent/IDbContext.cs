using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Common.Interfaces.Persistent;

/// <summary>
/// Used to access for db by application
/// </summary>
public interface IDbContext
{
    DbSet<User> Users { get; }

    DbSet<UserIpRule> UserIpRules { get; }

    DbSet<Meter> Meters { get; }

    DbSet<MeterEndpoint> MeterEndpoints { get; }

    DbSet<ProxyPort> ProxyPorts { get; }

    DbSet<ProxySession> ProxySessions { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);

}
