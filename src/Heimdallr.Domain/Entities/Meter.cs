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

    private Meter(string name, string? model, string? serialNumber)
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
        Name = name;
        Model = model;
        SerialNumber = serialNumber;
    }

    public static Meter Create(string name, string? model = null, string? serialNumber = null) => 
        new(name, model, serialNumber);
}
