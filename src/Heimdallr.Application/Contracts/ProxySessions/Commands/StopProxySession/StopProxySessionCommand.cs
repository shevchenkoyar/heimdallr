using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.ProxySessions.Commands.StartProxySession;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;

namespace Heimdallr.Application.Contracts.ProxySessions.Commands.StopProxySession;

public sealed record StopProxySessionCommand(
    Guid SessionId,
    Guid UserId) : ICommand<ProxySessionDto>;
