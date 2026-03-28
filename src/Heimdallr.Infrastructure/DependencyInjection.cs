using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Infrastructure.Database;
using Heimdallr.Infrastructure.Database.Data;
using Heimdallr.Infrastructure.Proxying;
using Heimdallr.Infrastructure.Proxying.Runtime;
using Heimdallr.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdallr.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration) =>
            services
                .AddAuthorizationServices()
                .AddDatabase(configuration)
                .AddProxyServices();

        private IServiceCollection AddAuthorizationServices()
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<User>>();
            
            return services;
        }

        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("heimdallr-db")));

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
            
            return services;
        }
        
        private IServiceCollection AddProxyServices()
        {
            services.AddSingleton<IProxyBridgeFactory, ProxyBridgeFactory>();
            services.AddSingleton<IProxyRuntimeManager, ProxyRuntimeManager>();
            
            return services;
        }
    }

    extension(IServiceProvider serviceProvider)
    {
        public void ApplyMigrations()
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
