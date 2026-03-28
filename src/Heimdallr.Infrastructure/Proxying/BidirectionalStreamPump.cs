namespace Heimdallr.Infrastructure.Proxying;

public static class BidirectionalStreamPump
{
    public static async Task<ProxyTransferResult> PumpAsync(
        Stream clientStream, 
        Stream targetStream, 
        CancellationToken cancellationToken)
    {
        DateTimeOffset startedAt = DateTimeOffset.UtcNow;

        long bytesFromClient = 0;
        long bytesFromTarget = 0;
        string? faultReason = null;
        
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        Task clientToTarget = CopyAsync(
            source: clientStream,
            destination: targetStream,
            onBytesCopied: copied => Interlocked.Add(ref bytesFromClient, copied),
            linkedCts.Token);
        
        Task targetToClient = CopyAsync(
            source: targetStream,
            destination: clientStream,
            onBytesCopied: copied => Interlocked.Add(ref bytesFromTarget, copied),
            linkedCts.Token);

        Task completed = await Task.WhenAny(clientToTarget, targetToClient);

        try
        {
            await completed;
        }
        catch (Exception ex)
        {
            faultReason = ex.Message;
        }
        
        await linkedCts.CancelAsync();

        try
        {
            await Task.WhenAll(
                IgnoreCancellation(clientToTarget),
                IgnoreCancellation(targetToClient));
        }
        catch (Exception ex) when(faultReason is null)
        {
            faultReason = ex.Message;
        }

        return new ProxyTransferResult(
            BytesFromClient: bytesFromClient,
            BytesFromTarget: bytesFromTarget,
            StartedAt: startedAt,
            EndedAt: DateTimeOffset.UtcNow,
            FaultReason: faultReason);
    }

    private static async Task CopyAsync(
        Stream source,
        Stream destination,
        Action<int> onBytesCopied,
        CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[81920];

        while (!cancellationToken.IsCancellationRequested)
        {
            int read = await source.ReadAsync(buffer, cancellationToken);
            if (read == 0)
            {
                break;
            }
            
            await destination.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            await destination.FlushAsync(cancellationToken);
            
            onBytesCopied(read);
        }
    }

    private static async Task IgnoreCancellation(Task task)
    {
        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            // Ignored
        }
        catch (ObjectDisposedException)
        {
            // Ignored
        }
        catch (IOException)
        {
            // Ignored
        }
    }
}
