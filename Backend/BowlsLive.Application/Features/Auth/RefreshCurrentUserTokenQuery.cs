using System.Security.Claims;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record RefreshCurrentUserTokenQuery(ClaimsPrincipal Principal) : IRequest<Result<AuthResponseDto>>;

public sealed class RefreshCurrentUserTokenQueryHandler(
    IAuthIdentityService authIdentityService,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RefreshCurrentUserTokenQuery, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(
        RefreshCurrentUserTokenQuery request,
        CancellationToken cancellationToken)
    {
        var user = await authIdentityService.GetCurrentUserAsync(request.Principal);

        if (user is null)
        {
            return Result<AuthResponseDto>.Unauthorized("The current user could not be resolved.");
        }

        var token = jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.Roles);
        var response = new AuthResponseDto(
            token.AccessToken,
            token.ExpiresAtUtc,
            new AuthenticatedUserDto(user.Id.ToString(), user.Email, user.UserName, user.Roles));

        return Result<AuthResponseDto>.Success(response);
    }
}

