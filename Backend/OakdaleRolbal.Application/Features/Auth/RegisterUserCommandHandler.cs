using MediatR;
using OakdaleRolbal.Application.Common.Results;
using OakdaleRolbal.Application.Interfaces;
using OakdaleRolbal.Application.Models.Auth;

namespace OakdaleRolbal.Application.Features.Auth;

public sealed class RegisterUserCommandHandler(
    IAuthIdentityService authIdentityService,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim();
        var userName = request.UserName.Trim();

        if (await authIdentityService.FindByEmailAsync(email, cancellationToken) is not null)
        {
            return Result<AuthResponseDto>.Validation(new Dictionary<string, string[]>
            {
                [nameof(request.Email)] = ["A user with this email already exists."]
            });
        }

        if (await authIdentityService.FindByUserNameAsync(userName, cancellationToken) is not null)
        {
            return Result<AuthResponseDto>.Validation(new Dictionary<string, string[]>
            {
                [nameof(request.UserName)] = ["That username is already taken."]
            });
        }

        var createResult = await authIdentityService.CreateUserAsync(
            email,
            userName,
            request.Password,
            cancellationToken);

        if (!createResult.Succeeded || createResult.User is null)
        {
            return Result<AuthResponseDto>.Validation(createResult.Errors);
        }

        return Result<AuthResponseDto>.Success(CreateAuthResponse(createResult.User));
    }

    private AuthResponseDto CreateAuthResponse(AuthIdentityUser user)
    {
        var token = jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName);
        return new AuthResponseDto(token.AccessToken, token.ExpiresAtUtc, ToUserDto(user));
    }

    private static AuthenticatedUserDto ToUserDto(AuthIdentityUser user)
    {
        return new AuthenticatedUserDto(user.Id.ToString(), user.Email, user.UserName);
    }
}
