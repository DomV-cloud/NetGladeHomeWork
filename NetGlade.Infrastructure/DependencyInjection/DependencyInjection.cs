using Microsoft.Extensions.DependencyInjection;
using NetGlade.Application.Common.Interfaces;
using NetGlade.Application.Services.Authentication;
using NetGlade.Infrastructure.Authentication;

namespace NetGlade.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtTokenGenerator, JwtGenerator>();
             
            return services;
        }
    }
}
