namespace Heimdallr.Infrastructure.Proxying;

public sealed record ProxyConnectionContext(
    string ClientIp,
    int ListeningPort,
    CancellationToken CancellationToken);
