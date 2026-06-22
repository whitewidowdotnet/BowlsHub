using System.ComponentModel.DataAnnotations;

namespace BowlsLive.Api.Contracts.Auth;

public sealed class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string UserName { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string ConfirmPassword { get; init; } = string.Empty;
}

public sealed class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}

public sealed record AuthenticatedUserResponse(string Id, string Email, string UserName, IReadOnlyList<string> Roles);

public sealed record AuthResponse(
    string AccessToken,
    DateTimeOffset ExpiresAtUtc,
    AuthenticatedUserResponse User);
