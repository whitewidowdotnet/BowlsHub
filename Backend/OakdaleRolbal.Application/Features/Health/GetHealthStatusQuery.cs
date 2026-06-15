using MediatR;

namespace OakdaleRolbal.Application.Features.Health;

public sealed record GetHealthStatusQuery : IRequest<GetHealthStatusResponse>;

