using FluentValidation;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Clubs;
using BowlsLive.Domain.Entities;

namespace BowlsLive.Application.Features.Clubs;

public sealed record CreateClubCommand(
    string Name,
    string ShortName,
    string Slug,
    string? Email,
    string? PhoneNumber) : IRequest<Result<ClubDto>>;

public sealed class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Club name is required.")
            .MaximumLength(200).WithMessage("Club name must not exceed 200 characters.");

        RuleFor(x => x.ShortName)
            .NotEmpty().WithMessage("Short name is required.")
            .MaximumLength(100).WithMessage("Short name must not exceed 100 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(100).WithMessage("Slug must not exceed 100 characters.")
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage("Slug must be lowercase letters, numbers, and hyphens only (e.g. 'my-club').");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email address is required.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50).WithMessage("Phone number must not exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}

public sealed class CreateClubCommandHandler(IClubRepository clubRepository)
    : IRequestHandler<CreateClubCommand, Result<ClubDto>>
{
    public async Task<Result<ClubDto>> Handle(
        CreateClubCommand request,
        CancellationToken cancellationToken)
    {
        var slug = request.Slug.Trim().ToLowerInvariant();

        if (await clubRepository.SlugExistsAsync(slug, cancellationToken))
        {
            return Result<ClubDto>.Validation(new Dictionary<string, string[]>
            {
                [nameof(request.Slug)] = [$"A club with the slug '{slug}' already exists."]
            });
        }

        var club = new Club
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            ShortName = request.ShortName.Trim(),
            Slug = slug,
            Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
            IsActive = true,
            CreatedUtc = DateTime.UtcNow
        };

        var created = await clubRepository.AddAsync(club, cancellationToken);
        return Result<ClubDto>.Success(ToDto(created));
    }

    private static ClubDto ToDto(Club club) =>
        new(club.Id, club.Name, club.ShortName, club.Slug, club.Email, club.PhoneNumber, club.IsActive, club.CreatedUtc);
}

