using System.ComponentModel.DataAnnotations;

namespace BowlsLive.Api.Contracts.Clubs;

public sealed class CreateClubRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ShortName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Slug { get; init; } = string.Empty;

    [EmailAddress]
    [MaxLength(256)]
    public string? Email { get; init; }

    [MaxLength(50)]
    public string? PhoneNumber { get; init; }
}

public sealed class UpdateClubRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ShortName { get; init; } = string.Empty;

    [EmailAddress]
    [MaxLength(256)]
    public string? Email { get; init; }

    [MaxLength(50)]
    public string? PhoneNumber { get; init; }

    public bool IsActive { get; init; } = true;
}

public sealed record ClubResponse(
    Guid Id,
    string Name,
    string ShortName,
    string Slug,
    string? Email,
    string? PhoneNumber,
    bool IsActive,
    DateTime CreatedUtc);

public sealed record ClubSummaryResponse(
    Guid Id,
    string Name,
    string ShortName,
    string Slug,
    bool IsActive);

