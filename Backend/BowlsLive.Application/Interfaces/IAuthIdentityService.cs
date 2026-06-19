using System.Security.Claims;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Application.Interfaces;

public interface IAuthIdentityService
{
    Task<AuthIdentityUser?> FindByEmailAsync(string email, CancellationToken cancellationToken);

    Task<AuthIdentityUser?> FindByUserNameAsync(string userName, CancellationToken cancellationToken);

    Task<CreateAuthIdentityUserResult> CreateUserAsync(
        string email,
        string userName,
        string password,
        CancellationToken cancellationToken);

    Task<AuthIdentityUser?> ValidateCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken);

    Task<AuthIdentityUser?> GetCurrentUserAsync(ClaimsPrincipal principal);
}
