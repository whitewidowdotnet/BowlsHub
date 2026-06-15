using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OakdaleRolbal.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<OakdaleRolbalDbContext>(options =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=oakdalerolbal;Username=postgres;Password=postgres");
                return;
            }

            options.UseNpgsql(connectionString);
        });

        return services;
    }
}

