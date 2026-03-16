using Heimdallr.Application;
using Heimdallr.Infrastructure;
using Heimdallr.Infrastructure.Database;
using Heimdallr.WebUI;
using Heimdallr.WebUI.Common.Extensions;
using Heimdallr.WebUI.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

await app.CreateBaseItems();

await app.RunAsync();
