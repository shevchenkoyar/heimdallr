using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Constants.ErrorCodes;
using Heimdallr.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.Users.Commands.Login;

[UsedImplicitly]
public class LoginCommandHandler(IDbContext context, IPasswordHasher hasher) : ICommandHandler<LoginCommand>
{
    private readonly Error _authorizationFailure = Error
        .NotFound(UserActionErrorCodes.AuthorizationFailed, "Cant login with this credentials");

    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        Result<User> userResult = await FindUserByLogin(command.Login, cancellationToken);

        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error);
        }
        
        string passwordHash = await hasher.HashAsync(command.Login, command.Password, cancellationToken);
        
        return userResult.Value.PasswordHash != passwordHash 
            ? Result.Failure(_authorizationFailure) 
            : Result.Success();
    }

    private async ValueTask<Result<User>> FindUserByLogin(string login, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.UserName == login, cancellationToken);

        return user == null 
            ? Result.Failure<User>(_authorizationFailure) 
            : Result.Success(user);
    }
}
