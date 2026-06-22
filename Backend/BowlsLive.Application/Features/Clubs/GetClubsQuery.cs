using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Clubs;
using BowlsLive.Domain.Entities;

namespace BowlsLive.Application.Features.Clubs;

public sealed record GetClubsQuery : IRequest<Result<IReadOnlyList<ClubSummaryDto>>>;

public sealed class GetClubsQueryHandler(IClubRepository clubRepository)
    : IRequestHandler<GetClubsQuery, Result<IReadOnlyList<ClubSummaryDto>>>
{
    public async Task<Result<IReadOnlyList<ClubSummaryDto>>> Handle(
        GetClubsQuery request,
        CancellationToken cancellationToken)
    {
        var clubs = await clubRepository.GetAllActiveAsync(cancellationToken);
        var dtos = clubs.Select(ToSummaryDto).ToList();
        return Result<IReadOnlyList<ClubSummaryDto>>.Success(dtos);
    }

    private static ClubSummaryDto ToSummaryDto(Club club) =>
        new(club.Id, club.Name, club.ShortName, club.Slug, club.IsActive);
}

