using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BowlsLive.Application.Interfaces;
using BowlsLive.Application.Models.Auth;

namespace BowlsLive.Persistence.Identity;

public sealed class AuthIdentityService(UserManager<ApplicationUser> userManager) : IAuthIdentityService
{
    public async Task<AuthIdentityUser?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);
        return ToModel(user);
    }

    public async Task<AuthIdentityUser?> FindByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(userName);
        return ToModel(user);
    }

    public async Task<CreateAuthIdentityUserResult> CreateUserAsync(
        string email,
        string userName,
        string password,
        CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = userName
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new CreateAuthIdentityUserResult(false, null, ToDictionary(result.Errors));
        }

        return new CreateAuthIdentityUserResult(true, ToModel(user), new Dictionary<string, string[]>());
    }

    public async Task<AuthIdentityUser?> ValidateCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return null;
        }

        var isValid = await userManager.CheckPasswordAsync(user, password);
        return isValid ? ToModel(user) : null;
    }

    public async Task<AuthIdentityUser?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        return ToModel(user);
    }

    private static AuthIdentityUser? ToModel(ApplicationUser? user)
    {
        if (user is null)
        {
            return null;
        }

        return new AuthIdentityUser(
            user.Id,
            user.Email ?? string.Empty,
            user.UserName ?? string.Empty);
    }

    private static Dictionary<string, string[]> ToDictionary(IEnumerable<IdentityError> errors)
    {
        return errors
            .GroupBy(error => error.Code, StringComparer.Ordinal)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray(),
                StringComparer.Ordinal);
    }
}
