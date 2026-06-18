namespace OakdaleRolbal.Application.Common.Authentication;

public sealed record JwtTokenResult(string AccessToken, DateTimeOffset ExpiresAtUtc);
