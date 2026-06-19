using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BowlsLive.Application.Interfaces;
using BowlsLive.Infrastructure.Authentication;

namespace BowlsLive.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
