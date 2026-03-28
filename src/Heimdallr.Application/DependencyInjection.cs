using System.Reflection;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces;
using Heimdallr.Application.Common.Time;
using Heimdallr.Application.Common.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdallr.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            return services
                .RegisterCommonServices()
                .RegisterCqrsHandlers(AssemblyReference.Assembly);
        }

        private IServiceCollection RegisterCommonServices()
        {
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

            return services;
        }

        private IServiceCollection RegisterCqrsHandlers(Assembly applicationAssembly)
        {
            return services.Scan(scan => scan.FromAssemblies(applicationAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
