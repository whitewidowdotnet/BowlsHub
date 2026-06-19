using Microsoft.AspNetCore.Identity;

namespace BowlsLive.Persistence.Identity;

public sealed class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; } = string.Empty;
    public bool IsSystemRole { get; set; }
}
