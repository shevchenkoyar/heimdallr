using Heimdallr.Application.Common.Monads;

namespace Heimdallr.Application.Common.Interfaces;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}

