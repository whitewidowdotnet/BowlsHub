using MediatR;
using OakdaleRolbal.Application.Common.Results;
using OakdaleRolbal.Application.Models.Auth;

namespace OakdaleRolbal.Application.Features.Auth;

public sealed record RegisterUserCommand(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthResponseDto>>;
