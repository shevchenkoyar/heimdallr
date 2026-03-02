namespace Heimdallr.Domain.Entities;

public sealed class Meter
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string? Model { get; set; }
    
    public string? SerialNumber { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<MeterEndpoint> Endpoints { get; set; } = new List<MeterEndpoint>();
    
    public ICollection<ProxySession> Sessions { get; set; } = new List<ProxySession>();
}
