using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetGlade.Application.Common.Interfaces;
using NetGlade.Application.Common.Interfaces.Services;
using NetGlade.Application.Services.Authentication;
using NetGlade.Infrastructure.Authentication;
using NetGlade.Infrastructure.Services;

namespace NetGlade.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IJwtTokenGenerator, JwtGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
             
            return services;
        }
    }
}
