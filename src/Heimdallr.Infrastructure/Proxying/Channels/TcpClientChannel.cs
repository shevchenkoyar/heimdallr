using System.Net.Sockets;

namespace Heimdallr.Infrastructure.Proxying.Channels;

public sealed class TcpClientChannel(string host, int port) : IByteChannel
{
    private readonly TcpClient _tcpClient = new();

    public Stream Input => _tcpClient.GetStream();
    
    public Stream Output => _tcpClient.GetStream();
    
    public async Task OpenAsync(CancellationToken cancellationToken)
    {
        await _tcpClient.ConnectAsync(host, port, cancellationToken);
    }
    
    public ValueTask DisposeAsync()
    {
        _tcpClient.Dispose();
        return ValueTask.CompletedTask;
    }
}
