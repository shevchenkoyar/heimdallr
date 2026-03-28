namespace Heimdallr.Infrastructure.Proxying.Channels;

public interface IByteChannel : IAsyncDisposable
{
    Stream Input { get; }
    
    Stream Output { get; }
    
    Task OpenAsync(CancellationToken cancellationToken);
}
