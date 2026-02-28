using MudBlazor.Services;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddPresenterDependencies()
        {
            return builder.AddUiLibs();
        }

        private WebApplicationBuilder AddUiLibs()
        {
            builder.Services.AddMudServices();
            return builder;
        }
    }
}
