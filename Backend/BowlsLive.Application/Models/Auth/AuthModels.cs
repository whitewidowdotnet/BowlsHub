namespace BowlsLive.Application.Models.Auth;

public sealed record AuthIdentityUser(Guid Id, string Email, string UserName);

public sealed record CreateAuthIdentityUserResult(
    bool Succeeded,
    AuthIdentityUser? User,
    Dictionary<string, string[]> Errors);

public sealed record AuthenticatedUserDto(string Id, string Email, string UserName);

public sealed record AuthResponseDto(
    string AccessToken,
    DateTimeOffset ExpiresAtUtc,
    AuthenticatedUserDto User);
