using System.IO.Ports;

namespace Heimdallr.Infrastructure.Proxying.Channels;

public sealed class SerialPortChannel(
    string portName,
    int baudRate,
    Parity parity,
    int dataBits,
    StopBits stopBits) : IByteChannel
{
    private readonly SerialPort _serialPort = new()
    {
        PortName = portName,
        BaudRate = baudRate,
        Parity = parity,
        DataBits = dataBits,
        StopBits = stopBits,
        ReadTimeout = 5000,
        WriteTimeout = 5000
    };
    
    public Stream Input => _serialPort.BaseStream;
    
    public Stream Output => _serialPort.BaseStream;
    
    public Task OpenAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _serialPort.Open();
        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        try
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
        finally
        {
            _serialPort.Dispose();
        }

        return ValueTask.CompletedTask;
    }
}
