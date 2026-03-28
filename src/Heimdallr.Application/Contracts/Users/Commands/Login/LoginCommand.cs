using System.Security.Claims;
using Heimdallr.Application.Common.Interfaces.Contracts;

namespace Heimdallr.Application.Contracts.Users.Commands.Login;

public record LoginCommand(string Login, string Password) : ICommand<List<Claim>>;
