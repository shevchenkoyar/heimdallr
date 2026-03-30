using System.Security.Claims;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Common.Time;
using Heimdallr.Application.Contracts.Users.Commands.Login;
using Heimdallr.WebUI.Services.Security;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace Heimdallr.WebUI.Endpoints;

[UsedImplicitly]
internal class Authorization : IEndpoint
{
    public string Endpoint => "/api/user/auth";

    public void Configure(WebApplication app)
    {
        app.MapPost(Endpoint, async (
                AuthorizationRequest request,
                ICommandHandler<LoginCommand, List<Claim>> handler,
                HttpContext httpContext,
                JwtTokenProvider jwtTokenProvider,
                IDateTimeProvider dateTimeProvider,
                CancellationToken token) =>
            {
                Result<List<Claim>> result =
                    await handler.Handle(new LoginCommand(request.Username, request.Password), token);

                if (result.IsFailure)
                {
                    return Results.Problem();
                }

                httpContext.Response.Cookies.Append(IdentityConstants.BearerScheme,
                    jwtTokenProvider.GetJwtToken(result.Value),
                    new CookieOptions
                {
                    HttpOnly = true,
                    Expires = dateTimeProvider.UtcNow.AddHours(12)
                });

                
                return result.IsSuccess ? Results.Ok() : Results.Problem();
            })
            .AllowAnonymous();
    }

    [UsedImplicitly]
    internal sealed class AuthorizationRequest
    {
        public string Username { get; init; }

        public string Password { get; init; }
    }
}
