namespace Heimdallr.Domain.Enums;

public enum ClientIpPolicy
{
    AllowAny = 1,
    PinToFirstIp = 2,
    AllowList = 3
}
