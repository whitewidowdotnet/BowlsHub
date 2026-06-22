using FluentValidation;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;

namespace BowlsLive.Application.Features.Clubs;

public sealed record RemoveClubCommand(Guid Id) : IRequest<Result<Guid>>;

public sealed class RemoveClubCommandValidator : AbstractValidator<RemoveClubCommand>
{
    public RemoveClubCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Club ID is required.");
    }
}

public sealed class RemoveClubCommandHandler(IClubRepository clubRepository)
    : IRequestHandler<RemoveClubCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RemoveClubCommand request, CancellationToken cancellationToken)
    {
        var club = await clubRepository.GetByIdAsync(request.Id, cancellationToken);

        if (club is null)
        {
            return Result<Guid>.NotFound($"No club was found with the ID '{request.Id}'.");
        }

        if (!club.IsActive)
        {
            return Result<Guid>.Success(club.Id);
        }

        club.IsActive = false;
        await clubRepository.UpdateAsync(club, cancellationToken);

        return Result<Guid>.Success(club.Id);
    }
}

