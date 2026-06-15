using Microsoft.EntityFrameworkCore;

namespace OakdaleRolbal.Persistence;

public sealed class OakdaleRolbalDbContext(DbContextOptions<OakdaleRolbalDbContext> options) : DbContext(options)
{
}

