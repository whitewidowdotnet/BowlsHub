using BowlsLive.Domain.Entities;

namespace BowlsLive.Application.Interfaces;

public interface IClubRepository
{
    Task<IReadOnlyList<Club>> GetAllActiveAsync(CancellationToken cancellationToken);

    Task<Club?> GetBySlugAsync(string slug, CancellationToken cancellationToken);

    Task<Club?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);

    Task<Club> AddAsync(Club club, CancellationToken cancellationToken);

    Task UpdateAsync(Club club, CancellationToken cancellationToken);
}

