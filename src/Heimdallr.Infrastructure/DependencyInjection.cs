using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Infrastructure.Database;
using Heimdallr.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdallr.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure() => 
            services
            .AddSecurityServices()
            .AddPersistence();

        private IServiceCollection AddSecurityServices()
        {
            services.AddTransient<IPasswordHasher, Sha256Pbkdf2PasswordHasher>();

            return services;
        }

        private IServiceCollection AddPersistence()
        {
            services.AddDbContext<IDbContext, ApplicationDbContext>();
            
            return services;
        }
    }
}
