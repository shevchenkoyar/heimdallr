using Heimdallr.WebUI.Endpoints;
using Heimdallr.WebUI.Services.Configuration;
using Heimdallr.WebUI.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Heimdallr.WebUI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation(IConfiguration configuration)
        {
            return services
                .CreateOptions(configuration)
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

        private IServiceCollection CreateOptions(IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }

        private IServiceCollection AddSecurityServices()
        {
            services.AddTransient<JwtTokenProvider>();

            ServiceProvider provider = services.BuildServiceProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                IOptions<JwtOptions> jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                string? token = context.Request.Cookies[IdentityConstants.BearerScheme];
                                if (!string.IsNullOrEmpty(token))
                                {
                                    context.Token = token;
                                }

                                return Task.CompletedTask;
                            }
                        };
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOptions.Value.Issuer,
                            ValidAudience = jwtOptions.Value.Audience,
                            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                                System.Text.Encoding.UTF8.GetBytes(jwtOptions.Value.SigningKey ??
                                                                   throw new InvalidOperationException(
                                                                       "Secret key must be provided.")))
                        };
                    });
            }

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
