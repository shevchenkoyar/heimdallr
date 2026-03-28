using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Infrastructure.Common.Extensions.Identity;
using Heimdallr.Infrastructure.Database;
using Heimdallr.Infrastructure.Database.Data;
using Microsoft.AspNetCore.Identity;

namespace Heimdallr.Infrastructure.Services;

internal class AuthorizationService(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
    : IAuthorizationService
{
    public async Task<Result> RegisterAsync(string login, string firstName, string lastName, string password,
        CancellationToken cancellationToken)
    {
        const string registrationFailureCode = "REGISTRATION_FAILURE";

        User? user = await userManager.FindByNameAsync(login);

        if (user != null)
        {
            return Result.Failure(new Error(registrationFailureCode, "This login is already exists.",
                ErrorType.Failure));
        }

        var newUser = User.Create(login, firstName, lastName);
        
        IdentityResult result = await userManager.CreateAsync(newUser, password);

        if (result.Failure)
        {
            return Result.Failure(new Error(registrationFailureCode, string.Join($";{Environment.NewLine}",
                result.Errors.Select(e => $"{{{e.Description}}}")), ErrorType.Failure));
        }
        
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task<Result> LoginAsync(string login, string password, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByNameAsync(login);

        if (user == null || !await userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure(new Error("AUTHORIZATION_FAILURE", "Your password or login is invalid.",
                ErrorType.Failure));
        }
        
        return Result.Success();
    }
}
