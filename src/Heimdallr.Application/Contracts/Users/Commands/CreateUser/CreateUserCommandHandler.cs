using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

[UsedImplicitly]
internal class CreateUserCommandHandler(IAuthorizationService authorizationService) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken) =>
        await authorizationService.RegisterAsync(command.Username, command.FirstName, command.LastName,
            command.Password, cancellationToken);
}
