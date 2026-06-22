using FluentValidation;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record RegisterUserCommand(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthResponseDto>>;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.UserName).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(command => command.Password).NotEmpty().MinimumLength(8);
        RuleFor(command => command.ConfirmPassword).NotEmpty().MinimumLength(8);
        RuleFor(command => command.ConfirmPassword)
            .Equal(command => command.Password)
            .WithMessage("Password confirmation does not match.");
    }
}

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
            return Result<AuthResponseDto>.Validation(new Dictionary<string, string[]>{
                [nameof(request.Email)] = ["A user with this email already exists."]
            });
        }

        if (await authIdentityService.FindByUserNameAsync(userName, cancellationToken) is not null)
        {
            return Result<AuthResponseDto>.Validation(new Dictionary<string, string[]>{
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
        var token = jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.Roles);
        return new AuthResponseDto(token.AccessToken, token.ExpiresAtUtc, ToUserDto(user));
    }

    private static AuthenticatedUserDto ToUserDto(AuthIdentityUser user) =>
        new(user.Id.ToString(), user.Email, user.UserName, user.Roles);
}
