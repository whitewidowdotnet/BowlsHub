using MediatR;
using Microsoft.AspNetCore.Mvc;
using OakdaleRolbal.Application.Features.Health;

namespace OakdaleRolbal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetHealthStatusResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetHealthStatusResponse>> Get(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetHealthStatusQuery(), cancellationToken);
        return Ok(response);
    }
}

