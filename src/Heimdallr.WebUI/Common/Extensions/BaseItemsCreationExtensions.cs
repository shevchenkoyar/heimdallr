using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.Users.Commands.CreateUser;

namespace Heimdallr.WebUI.Common.Extensions;

internal static class BaseItemsCreationExtensions
{
    public static async Task CreateBaseItems(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        await CreateAdmin(scope);
    }

    private static async Task CreateAdmin(IServiceScope scope)
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));

        ICommandHandler<CreateFirstAdminUserCommand> createAdminCommand =
            scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateFirstAdminUserCommand>>();

        await createAdminCommand.Handle(new CreateFirstAdminUserCommand(), cancellationTokenSource.Token);
    }
}
