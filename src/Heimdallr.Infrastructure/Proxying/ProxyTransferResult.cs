namespace Heimdallr.Infrastructure.Proxying;

public sealed record ProxyTransferResult(
    long BytesFromClient,
    long BytesFromTarget,
    DateTimeOffset StartedAt,
    DateTimeOffset EndedAt,
    string? FaultReason)
{
    public bool IsSuccess => FaultReason is null;
}
