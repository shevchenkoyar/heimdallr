using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;
using Heimdallr.Domain.Enums;

namespace Heimdallr.Application.Contracts.ProxySessions.Commands.StartProxySession;

public sealed record StartProxySessionCommand(
    Guid UserId,
    Guid MeterId,
    ClientIpPolicy ClientIpPolicy,
    TimeSpan LeaseDuration) : ICommand<ProxySessionDto>;
