using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record RegisterUserCommand(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthResponseDto>>;
