using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetGlade.Application.Common.Interfaces;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Application.Common.Interfaces.Services;
using NetGlade.Application.Services.Authentication;
using NetGlade.Infrastructure.Authentication;
using NetGlade.Infrastructure.Persistance;
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

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            return services;
        }
    }
}
