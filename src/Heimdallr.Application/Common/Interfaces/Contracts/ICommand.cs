namespace Heimdallr.Application.Common.Interfaces.Contracts;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;

public interface IBaseCommand;
