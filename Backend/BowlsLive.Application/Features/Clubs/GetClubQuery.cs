using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Clubs;
using BowlsLive.Domain.Entities;

namespace BowlsLive.Application.Features.Clubs;

public sealed record GetClubQuery(string Slug) : IRequest<Result<ClubDto>>;

public sealed class GetClubQueryHandler(IClubRepository clubRepository)
    : IRequestHandler<GetClubQuery, Result<ClubDto>>
{
    public async Task<Result<ClubDto>> Handle(
        GetClubQuery request,
        CancellationToken cancellationToken)
    {
        var club = await clubRepository.GetBySlugAsync(request.Slug, cancellationToken);

        if (club is null)
        {
            return Result<ClubDto>.NotFound($"No club was found with the slug '{request.Slug}'.");
        }

        return Result<ClubDto>.Success(ToDto(club));
    }

    private static ClubDto ToDto(Club club) =>
        new(club.Id, club.Name, club.ShortName, club.Slug, club.Email, club.PhoneNumber, club.IsActive, club.CreatedUtc);
}

