using Heimdallr.WebUI.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            return services
                .AddEndpoints()
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
                .AddCookie(IdentityConstants.BearerScheme, options => options.LoginPath = "/auth")
                .AddBearerToken(IdentityConstants.BearerScheme);
            
            services.AddAuthorization();
            
            return services;
        }

        private IServiceCollection AddEndpoints()
        {
            ServiceDescriptor[] serviceDescriptors = AssemblyReference.Assembly
                .GetTypes()
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }
    }
}
