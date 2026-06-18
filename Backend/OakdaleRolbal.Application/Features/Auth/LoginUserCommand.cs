using MediatR;
using OakdaleRolbal.Application.Common.Results;
using OakdaleRolbal.Application.Models.Auth;

namespace OakdaleRolbal.Application.Features.Auth;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<AuthResponseDto>>;
