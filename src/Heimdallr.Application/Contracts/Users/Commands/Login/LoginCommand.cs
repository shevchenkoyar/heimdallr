using ICommand = Heimdallr.Application.Common.Interfaces.Contracts.ICommand;

namespace Heimdallr.Application.Contracts.Users.Commands.Login;

public record LoginCommand(string Login, string Password) : ICommand;
