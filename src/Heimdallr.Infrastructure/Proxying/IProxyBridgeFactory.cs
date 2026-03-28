using Heimdallr.Domain.Entities;

namespace Heimdallr.Infrastructure.Proxying;

public interface IProxyBridgeFactory
{
    IProxyBridge Create(MeterEndpoint meterEndpoint);
}
