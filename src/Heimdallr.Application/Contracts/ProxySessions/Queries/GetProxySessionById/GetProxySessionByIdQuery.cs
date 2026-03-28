using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;

namespace Heimdallr.Application.Contracts.ProxySessions.Queries.GetProxySessionById;

public sealed record GetProxySessionByIdQuery(Guid SessionId) : IQuery<ProxySessionDto>;
