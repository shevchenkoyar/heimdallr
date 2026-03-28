using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using JetBrains.Annotations;

namespace Heimdallr.Application.Contracts.Users.Commands.Login;

[UsedImplicitly]
internal class LoginCommandHandler(IAuthorizationService authorizationService) : ICommandHandler<LoginCommand>
{
    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken) => 
        await authorizationService.LoginAsync(command.Login, command.Password, cancellationToken);
}
