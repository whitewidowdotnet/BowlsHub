namespace BowlsLive.Application.Models.Clubs;

public sealed record ClubDto(
    Guid Id,
    string Name,
    string ShortName,
    string Slug,
    string? Email,
    string? PhoneNumber,
    bool IsActive,
    DateTime CreatedUtc);

public sealed record ClubSummaryDto(
    Guid Id,
    string Name,
    string ShortName,
    string Slug,
    bool IsActive);

