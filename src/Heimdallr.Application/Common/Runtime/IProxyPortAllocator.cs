namespace Heimdallr.Application.Common.Runtime;

public interface IProxyPortAllocator
{
    Task<int> ReserveFreePortAsync(CancellationToken cancellationToken = default);
    
    Task MarkPortInUseAsync(int port, CancellationToken cancellationToken = default);
    
    Task ReleasePortAsync(int port, CancellationToken cancellationToken = default);
}
