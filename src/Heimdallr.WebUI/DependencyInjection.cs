using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.WebUI.Services.Security;
using Microsoft.AspNetCore.Identity;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            return services
                .AddUiLibs()
                .AddSecurityServices();
        }

        private IServiceCollection AddUiLibs()
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents();
            
            return services;
        }

        private IServiceCollection AddSecurityServices()
        {
            services.AddScoped<IUserSession, UserSession>();

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
            
            services.AddAuthorization();
            
            return services;
        }
    }
}
