using FluentValidation;

namespace OakdaleRolbal.Application.Features.Auth;

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
