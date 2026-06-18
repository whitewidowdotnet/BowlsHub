using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OakdaleRolbal.Persistence;

public sealed class OakdaleRolbalDbContextFactory : IDesignTimeDbContextFactory<OakdaleRolbalDbContext>
{
    public OakdaleRolbalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OakdaleRolbalDbContext>();
        const string connectionString =
            "Host=localhost;Port=5432;Database=oakdalerolbal;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);

        return new OakdaleRolbalDbContext(optionsBuilder.Options);
    }
}
