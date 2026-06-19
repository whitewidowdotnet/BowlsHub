using Microsoft.AspNetCore.Identity;
using BowlsLive.Domain.Enumerations;

namespace BowlsLive.Persistence.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserGender Gender { get; set; } = UserGender.Unspecified;
    public string? BsaRegistrationNumber { get; set; }
    public string? WpbaRegistrationNumber { get; set; }
    public DateTime? RegisteredBowlerSinceUtc { get; set; }
    public bool IsActive { get; set; } = true;
}
