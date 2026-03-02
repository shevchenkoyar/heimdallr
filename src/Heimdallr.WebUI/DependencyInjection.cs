using MudBlazor.Services;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services.AddUiLibs();
    }

    private static IServiceCollection AddUiLibs(this IServiceCollection services)
    {
        services.AddMudServices();
        return services;
    }
}
