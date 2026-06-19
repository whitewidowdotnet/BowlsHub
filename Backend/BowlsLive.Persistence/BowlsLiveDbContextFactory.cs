using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BowlsLive.Persistence;

public sealed class BowlsLiveDbContextFactory : IDesignTimeDbContextFactory<BowlsLiveDbContext>
{
    public BowlsLiveDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BowlsLiveDbContext>();
        const string connectionString =
            "Host=localhost;Port=5432;Database=bowlslive;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);

        return new BowlsLiveDbContext(optionsBuilder.Options);
    }
}
