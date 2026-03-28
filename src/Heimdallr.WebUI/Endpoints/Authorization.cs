using System.Security.Claims;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Users.Commands.Login;
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
                CancellationToken token) =>
            {
                Result result = await handler.Handle(new LoginCommand(request.Username, request.Password), token);
                httpContext.Response.Cookies.Append(IdentityConstants.BearerScheme, "token", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
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
