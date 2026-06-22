using FluentValidation;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Clubs;
using BowlsLive.Domain.Entities;

namespace BowlsLive.Application.Features.Clubs;

public sealed record UpdateClubCommand(
    Guid Id,
    string Name,
    string ShortName,
    string? Email,
    string? PhoneNumber,
    bool IsActive) : IRequest<Result<ClubDto>>;

public sealed class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
{
    public UpdateClubCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Club ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Club name is required.")
            .MaximumLength(200).WithMessage("Club name must not exceed 200 characters.");

        RuleFor(x => x.ShortName)
            .NotEmpty().WithMessage("Short name is required.")
            .MaximumLength(100).WithMessage("Short name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email address is required.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50).WithMessage("Phone number must not exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}

public sealed class UpdateClubCommandHandler(IClubRepository clubRepository)
    : IRequestHandler<UpdateClubCommand, Result<ClubDto>>
{
    public async Task<Result<ClubDto>> Handle(
        UpdateClubCommand request,
        CancellationToken cancellationToken)
    {
        var club = await clubRepository.GetByIdAsync(request.Id, cancellationToken);

        if (club is null)
        {
            return Result<ClubDto>.NotFound($"No club was found with the ID '{request.Id}'.");
        }

        club.Name = request.Name.Trim();
        club.ShortName = request.ShortName.Trim();
        club.Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();
        club.PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim();
        club.IsActive = request.IsActive;

        await clubRepository.UpdateAsync(club, cancellationToken);
        return Result<ClubDto>.Success(ToDto(club));
    }

    private static ClubDto ToDto(Club club) =>
        new(club.Id, club.Name, club.ShortName, club.Slug, club.Email, club.PhoneNumber, club.IsActive, club.CreatedUtc);
}

