namespace Heimdallr.Infrastructure.Proxying.Runtime;

public interface IProxySessionRuntime : IAsyncDisposable
{
    Guid SessionId { get; }
    
    int ListeningPort { get; }

    Task StartAsync(CancellationToken cancellationToken = default);
    
    Task StopAsync(CancellationToken cancellationToken = default);
}
