using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<AuthResponseDto>>;
