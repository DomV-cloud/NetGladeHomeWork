using NetGlade.Infrastructure.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace NetGlade.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
