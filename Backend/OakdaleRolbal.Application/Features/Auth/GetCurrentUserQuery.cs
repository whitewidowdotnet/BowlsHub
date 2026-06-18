using System.Security.Claims;
using MediatR;
using OakdaleRolbal.Application.Common.Results;
using OakdaleRolbal.Application.Models.Auth;

namespace OakdaleRolbal.Application.Features.Auth;

public sealed record GetCurrentUserQuery(ClaimsPrincipal Principal) : IRequest<Result<AuthenticatedUserDto>>;
