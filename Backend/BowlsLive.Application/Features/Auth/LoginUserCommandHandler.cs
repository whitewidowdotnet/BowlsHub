using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed class LoginUserCommandHandler(
    IAuthIdentityService authIdentityService,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginUserCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await authIdentityService.ValidateCredentialsAsync(
            request.Email.Trim(),
            request.Password,
            cancellationToken);

        if (user is null)
        {
            return Result<AuthResponseDto>.Unauthorized("Invalid email or password.");
        }

        var token = jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName);
        var response = new AuthResponseDto(
            token.AccessToken,
            token.ExpiresAtUtc,
            new AuthenticatedUserDto(user.Id.ToString(), user.Email, user.UserName));

        return Result<AuthResponseDto>.Success(response);
    }
}
