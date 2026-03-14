using Heimdallr.Application.Common.Interfaces.Contracts;

namespace Heimdallr.Application.Contracts.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Password) : ICommand;
