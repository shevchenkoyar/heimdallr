using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.Users.Commands;

[UsedImplicitly]
public class AuthorizeCommandHandler(IDbContext context, IPasswordHasher hasher) : ICommandHandler<AuthorizeCommand>
{
    private readonly Error _authorizationFaulure = Error.NotFound("AUTHORIZATION_FAILURE", "Cant log in with this credentials");
    
    public async Task<Result> Handle(AuthorizeCommand command, CancellationToken cancellationToken)
    {
        Result<User> userResult = await FindUserByLogin(command.Login, cancellationToken);

        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error);
        }
        
        string passwordHash = await hasher.HashAsync(command.Password, cancellationToken);

        return userResult.Value.PasswordHash != passwordHash 
            ? Result.Failure(_authorizationFaulure) 
            : Result.Success();
    }

    private async ValueTask<Result<User>> FindUserByLogin(string login, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.UserName == login, cancellationToken);

        return user == null 
            ? Result.Failure<User>(_authorizationFaulure) 
            : Result.Success(user);
    }
}
