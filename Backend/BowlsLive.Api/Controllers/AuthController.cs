using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BowlsLive.Api.Contracts.Auth;
using BowlsLive.Application.Common.Results;
using BowlsLive.Application.Features.Auth;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new RegisterUserCommand(request.Email, request.UserName, request.Password, request.ConfirmPassword),
            cancellationToken);

        return ToAuthResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginUserCommand(request.Email, request.Password), cancellationToken);
        return ToAuthResponse(result);
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthenticatedUserResponse>> Me(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCurrentUserQuery(User), cancellationToken);

        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(ToUserResponse(result.Value));
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    [Authorize]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Refresh(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshCurrentUserTokenQuery(User), cancellationToken);
        return ToAuthResponse(result);
    }

    private ActionResult<AuthResponse> ToAuthResponse(Result<AuthResponseDto> result)
    {
        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(new AuthResponse(
                result.Value.AccessToken,
                result.Value.ExpiresAtUtc,
                ToUserResponse(result.Value.User)));
        }

        return ToErrorResult(result.ErrorType, result.ErrorMessage, result.ValidationErrors);
    }

    private ActionResult ToErrorResult(
        ResultErrorType? errorType,
        string? errorMessage,
        Dictionary<string, string[]>? validationErrors)
    {
        return errorType switch
        {
            ResultErrorType.Validation => ValidationProblem(
                new ValidationProblemDetails(validationErrors ?? new Dictionary<string, string[]>())),
            ResultErrorType.Unauthorized => Unauthorized(new ProblemDetails
            {
                Title = errorMessage ?? "Unauthorized."
            }),
            ResultErrorType.NotFound => NotFound(new ProblemDetails
            {
                Title = errorMessage ?? "The requested resource was not found."
            }),
            _ => BadRequest(new ProblemDetails
            {
                Title = errorMessage ?? "The request could not be completed."
            })
        };
    }

    private static AuthenticatedUserResponse ToUserResponse(AuthenticatedUserDto user)
    {
        return new AuthenticatedUserResponse(user.Id, user.Email, user.UserName, user.Roles);
    }
}
