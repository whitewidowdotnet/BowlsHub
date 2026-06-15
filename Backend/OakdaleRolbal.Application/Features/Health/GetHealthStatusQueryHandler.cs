using MediatR;

namespace OakdaleRolbal.Application.Features.Health;

public sealed class GetHealthStatusQueryHandler : IRequestHandler<GetHealthStatusQuery, GetHealthStatusResponse>
{
    public Task<GetHealthStatusResponse> Handle(GetHealthStatusQuery request, CancellationToken cancellationToken)
    {
        var response = new GetHealthStatusResponse("Healthy", DateTime.UtcNow);
        return Task.FromResult(response);
    }
}

