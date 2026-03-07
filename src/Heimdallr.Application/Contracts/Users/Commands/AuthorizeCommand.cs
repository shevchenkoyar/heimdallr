using ICommand = Heimdallr.Application.Common.Interfaces.Contracts.ICommand;

namespace Heimdallr.Application.Contracts.Users.Commands;

public record AuthorizeCommand(string Login, string Password) : ICommand;
