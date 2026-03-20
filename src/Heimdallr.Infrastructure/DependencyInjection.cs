using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Interfaces.Security;
using Heimdallr.Infrastructure.Database;
using Heimdallr.Infrastructure.Database.Data;
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
                .AddSecurityServices()
                .AddDatabase(configuration);
        
        private IServiceCollection AddSecurityServices()
        {
            services.AddTransient<IPasswordHasher, Sha256Pbkdf2PasswordHasher>();
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            return services;
        }

        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("heimdallr-db")));

            services.AddDbContext<IDbContext, ApplicationDbContext>();
            
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
