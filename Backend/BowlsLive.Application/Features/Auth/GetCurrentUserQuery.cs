using System.Security.Claims;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record GetCurrentUserQuery(ClaimsPrincipal Principal) : IRequest<Result<AuthenticatedUserDto>>;

public sealed class GetCurrentUserQueryHandler(IAuthIdentityService authIdentityService)
    : IRequestHandler<GetCurrentUserQuery, Result<AuthenticatedUserDto>>
{
    public async Task<Result<AuthenticatedUserDto>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await authIdentityService.GetCurrentUserAsync(request.Principal);

        if (user is null)
        {
            return Result<AuthenticatedUserDto>.Unauthorized("The current user could not be resolved.");
        }

        return Result<AuthenticatedUserDto>.Success(new AuthenticatedUserDto(user.Id.ToString(), user.Email, user.UserName, user.Roles));
    }
}
