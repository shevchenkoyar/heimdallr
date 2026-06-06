namespace Heimdallr.Application.Contracts.Meters.Dtos;

public sealed class MeterDto(
    Guid meterId,
    string name,
    string? model,
    string? serialNumber,
    bool isEnabled,
    DateTimeOffset createdAt,
    DateTimeOffset updatedAt)
{
    public Guid MeterId { get; } = meterId;

    public string Name { get; } = name;

    public string? Model { get;  } = model;

    public string? SerialNumber { get; } = serialNumber;

    public bool IsEnabled { get; } = isEnabled;

    public DateTimeOffset CreatedAt { get; } = createdAt;

    public DateTimeOffset UpdatedAt { get; } = updatedAt;
}
