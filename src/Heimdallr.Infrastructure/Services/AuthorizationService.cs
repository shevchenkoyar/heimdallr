using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Infrastructure.Database.Data;
using Microsoft.AspNetCore.Identity;

namespace Heimdallr.Infrastructure.Services;

internal class AuthorizationService(UserManager<ApplicationUser> userManager) : IAuthorizationService
{
    public ApplicationUser? AuthorizedUser { get; private set; }
    
    public async Task<Result> RegisterAsync(string login, string password, CancellationToken token)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(login);

        if (user != null)
        {
            return Result.Failure(new Error("REGISTRATION_FAILURE", "This login is already exists.", 
                ErrorType.Failure));
        }

        var newUser = new ApplicationUser
        {
            UserName = login,
        };
        
        IdentityResult result = await userManager.CreateAsync(newUser, password);

        if (result.Succeeded)
        {
            AuthorizedUser = newUser;
            return Result.Success();
        }
        
        return Result.Failure(new Error("REGISTRATION_FAILURE", string.Join($";{Environment.NewLine}", 
            result.Errors.Select(e => $"{{{e.Description}}}")), ErrorType.Failure));
    }

    public async Task<Result> LoginAsync(string login, string password, CancellationToken token)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(login);

        if (user == null || !await userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure(new Error("AUTHORIZATION_FAILURE", "Your password or login is invalid.", 
                ErrorType.Failure));
        }
        
        AuthorizedUser = user;
        
        return Result.Success();
    }
}
