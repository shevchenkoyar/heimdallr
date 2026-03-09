using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.WebUI.Services.Security;
using MudBlazor.Services;
using Serilog;
using Serilog.Core;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            return services.AddUiLibs();
        }

        private IServiceCollection AddUiLibs()
        {
            services.AddMudServices();
            return services;
        }

        private IServiceCollection AddSecurityServices()
        {
            services.AddScoped<IUserSession, UserSession>();
            
            return services;
        }
    }
}
