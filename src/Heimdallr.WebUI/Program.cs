using Heimdallr.Application;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.Users.Commands.CreateUser;
using Heimdallr.Infrastructure;
using Heimdallr.Infrastructure.Database;
using Heimdallr.WebUI;
using Heimdallr.WebUI.Components;
using Heimdallr.WebUI.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddNpgsqlDbContext<ApplicationDbContext>(connectionName: "heimdallr-db");

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();

ICommandHandler<CreateFirstAdminUserCommand> createAdminCommand =
    app.Services.GetRequiredService<ICommandHandler<CreateFirstAdminUserCommand>>();

using (var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
{
    await createAdminCommand.Handle(new CreateFirstAdminUserCommand(), cancellationTokenSource.Token);
}
