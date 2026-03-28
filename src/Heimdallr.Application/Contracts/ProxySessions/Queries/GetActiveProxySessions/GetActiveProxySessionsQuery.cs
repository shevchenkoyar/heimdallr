using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.ProxySessions.Dtos;

namespace Heimdallr.Application.Contracts.ProxySessions.Queries.GetActiveProxySessions;

public sealed record GetActiveProxySessionsQuery(Guid? UserId = null) : IQuery<IReadOnlyList<ProxySessionDto>>;
