using MediatR;

namespace BowlsLive.Application.Features.Health;

public sealed record GetHealthStatusQuery : IRequest<GetHealthStatusResponse>;

