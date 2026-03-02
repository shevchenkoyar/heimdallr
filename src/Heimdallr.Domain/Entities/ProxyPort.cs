using Heimdallr.Domain.Enums;

namespace Heimdallr.Domain.Entities;

public sealed class ProxyPort
{
    public int Port { get; set; }

    public ProxyPortState State { get; set; } = ProxyPortState.Free;

    public DateTimeOffset? ReservedAt { get; set; }
    
    public DateTimeOffset? LastUsedAt { get; set; }

    public ProxySession? CurrentSession { get; set; }
}
