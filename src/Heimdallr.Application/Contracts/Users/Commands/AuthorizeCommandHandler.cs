using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands;

[UsedImplicitly]
public class AuthorizeCommandHandler() : ICommandHandler<AuthorizeCommand>
{
    public Task<Result> Handle(AuthorizeCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Failure(Error.Failure("INVALID_DATA", "Incorrect data for authorization")));
    }
}
