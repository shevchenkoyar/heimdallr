using Heimdallr.Domain.Enums;

namespace Heimdallr.Domain.Entities;

public sealed class MeterEndpoint
{
    public Guid Id { get; set; }

    public Guid MeterId { get; set; }
    
    public Meter Meter { get; set; }

    public TransportType TransportType { get; set; } = TransportType.Tcp;

    public string Host { get; set; }
    
    public int? Port { get; set; }
    
    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
}
