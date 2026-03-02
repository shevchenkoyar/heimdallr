using Heimdallr.Domain.Interfaces.Persistence.Repositories;
using Heimdallr.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdallr.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure()
        {
            services.AddRepositories();
        
            return services;
        }

        private IServiceCollection AddRepositories()
        {
            services.AddScoped<IMeterRepository, EfMeterRepository>();
            services.AddScoped<IMeterEndpointRepository, EfMeterEndpointRepository>();
        
            services.AddScoped<IUserIpRuleRepository, EfUserIpRuleRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();
        
            services.AddScoped<IProxyPortRepository, EfProxyPortRepository>();
            services.AddScoped<IProxySessionRepository, EfProxySessionRepository>();
            return services;
        }
    }
}
