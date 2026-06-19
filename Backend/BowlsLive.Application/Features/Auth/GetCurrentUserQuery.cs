using System.Security.Claims;
using MediatR;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Features.Auth;

public sealed record GetCurrentUserQuery(ClaimsPrincipal Principal) : IRequest<Result<AuthenticatedUserDto>>;
