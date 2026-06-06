namespace Heimdallr.Application.Contracts.Meters.Dtos;

public class MeterEndpointDto(
    Guid id,
    string host,
    int port,
    bool isEnabled,
    bool isPrimary)
{
    public Guid Id { get; } = id;
    
    public string Host { get; } = host;

    public int Port { get; } = port;

    public bool IsEnabled { get; } = isEnabled;

    public bool IsPrimary { get; } = isPrimary;
}
