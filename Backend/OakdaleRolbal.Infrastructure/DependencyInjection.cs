using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OakdaleRolbal.Application.Interfaces;
using OakdaleRolbal.Infrastructure.Authentication;

namespace OakdaleRolbal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
