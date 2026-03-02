using Heimdallr.Domain.Enums;

namespace Heimdallr.Domain.Entities;

public sealed class ProxySession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public User User { get; set; }

    public Guid MeterId { get; set; }
    
    public Meter Meter { get; set; }
    
    public int ProxyPort { get; set; }
    
    public ProxyPort Port { get; set; }

    public SessionStatus Status { get; set; } = SessionStatus.Starting;

    public ClientIpPolicy ClientIpPolicy { get; set; } = ClientIpPolicy.AllowAny;
    
    public string? PinnedClientIp { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }
    
    public DateTimeOffset? LastActivityAt { get; set; }
    
    public DateTimeOffset? LeaseUntil { get; set; }
    
    public DateTimeOffset? EndedAt { get; set; }

    public long BytesFromClient { get; set; }
    
    public long BytesFromMeter { get; set; }

    public string? FaultReason { get; set; }
}
