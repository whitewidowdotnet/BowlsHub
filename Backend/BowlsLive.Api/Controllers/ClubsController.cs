using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BowlsLive.Api.Contracts.Clubs;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Features.Clubs;
using BowlsLive.Application.Models.Clubs;
using BowlsLive.Persistence.Identity;

namespace BowlsLive.Api.Controllers;

[ApiController]
[Route("api/clubs")]
public sealed class ClubsController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClubSummaryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClubSummaryResponse>>> GetClubs(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetClubsQuery(), cancellationToken);

        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(result.Value.Select(ToSummaryResponse).ToList());
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    [AllowAnonymous]
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ClubResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClubResponse>> GetClub(string slug, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetClubQuery(slug), cancellationToken);

        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(ToResponse(result.Value));
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    [Authorize(Roles = ApplicationRoles.InternalAdmin)]
    [HttpPost]
    [ProducesResponseType(typeof(ClubResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClubResponse>> CreateClub(CreateClubRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateClubCommand(request.Name, request.ShortName, request.Slug, request.Email, request.PhoneNumber),
            cancellationToken);

        if (result.IsSuccess && result.Value is not null)
        {
            var response = ToResponse(result.Value);
            return CreatedAtAction(nameof(GetClub), new { slug = response.Slug }, response);
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    [Authorize(Roles = $"{ApplicationRoles.InternalAdmin},{ApplicationRoles.ClubAdmin}")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ClubResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClubResponse>> UpdateClub(
        Guid id,
        UpdateClubRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateClubCommand(id, request.Name, request.ShortName, request.Email, request.PhoneNumber, request.IsActive),
            cancellationToken);

        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(ToResponse(result.Value));
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    [Authorize(Roles = ApplicationRoles.InternalAdmin)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveClub(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveClubCommand(id), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    private static ClubResponse ToResponse(ClubDto dto) =>
        new(dto.Id, dto.Name, dto.ShortName, dto.Slug, dto.Email, dto.PhoneNumber, dto.IsActive, dto.CreatedUtc);

    private static ClubSummaryResponse ToSummaryResponse(ClubSummaryDto dto) =>
        new(dto.Id, dto.Name, dto.ShortName, dto.Slug, dto.IsActive);

    private ActionResult ToErrorResult(
        ResultErrorType? errorType,
        string? errorMessage,
        Dictionary<string, string[]>? validationErrors)
    {
        return errorType switch
        {
            ResultErrorType.Validation => ValidationProblem(
                new ValidationProblemDetails(validationErrors ?? new Dictionary<string, string[]>())),
            ResultErrorType.Unauthorized => Unauthorized(new ProblemDetails { Title = errorMessage ?? "Unauthorized." }),
            ResultErrorType.NotFound => NotFound(new ProblemDetails { Title = errorMessage ?? "Not found." }),
            _ => BadRequest(new ProblemDetails { Title = errorMessage ?? "The request could not be completed." })
        };
    }
}

