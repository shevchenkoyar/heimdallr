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
            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
            
            services.AddAuthorization();
            
            return services;
        }
    }
}
