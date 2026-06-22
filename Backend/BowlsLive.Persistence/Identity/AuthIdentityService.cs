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
        return await ToModelAsync(user);
    }

    public async Task<AuthIdentityUser?> FindByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(userName);
        return await ToModelAsync(user);
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

        var roleResult = await userManager.AddToRoleAsync(user, ApplicationRoles.ClubMember);

        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return new CreateAuthIdentityUserResult(false, null, ToDictionary(roleResult.Errors));
        }

        return new CreateAuthIdentityUserResult(true, await ToModelAsync(user), new Dictionary<string, string[]>());
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
        return isValid ? await ToModelAsync(user) : null;
    }

    public async Task<AuthIdentityUser?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        return await ToModelAsync(user);
    }

    private async Task<AuthIdentityUser?> ToModelAsync(ApplicationUser? user)
    {
        if (user is null)
        {
            return null;
        }

        var roles = await userManager.GetRolesAsync(user);

        return new AuthIdentityUser(
            user.Id,
            user.Email ?? string.Empty,
            user.UserName ?? string.Empty,
            roles.ToArray());
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
