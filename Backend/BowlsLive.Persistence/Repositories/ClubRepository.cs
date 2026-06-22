using Microsoft.EntityFrameworkCore;
using BowlsLive.Application.Interfaces;
using BowlsLive.Domain.Entities;

namespace BowlsLive.Persistence.Repositories;

public sealed class ClubRepository(BowlsLiveDbContext dbContext) : IClubRepository
{
    public async Task<IReadOnlyList<Club>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Clubs
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Club?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await dbContext.Clubs
            .FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);
    }

    public async Task<Club?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Clubs
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken)
    {
        return await dbContext.Clubs
            .AnyAsync(c => c.Slug == slug, cancellationToken);
    }

    public async Task<Club> AddAsync(Club club, CancellationToken cancellationToken)
    {
        dbContext.Clubs.Add(club);
        await dbContext.SaveChangesAsync(cancellationToken);
        return club;
    }

    public async Task UpdateAsync(Club club, CancellationToken cancellationToken)
    {
        dbContext.Clubs.Update(club);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

