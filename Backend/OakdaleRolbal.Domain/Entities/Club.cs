using OakdaleRolbal.Domain.Common;

namespace OakdaleRolbal.Domain.Entities;

public sealed class Club : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public ICollection<ClubMembership> Memberships { get; set; } = new List<ClubMembership>();
    public ICollection<CompetitionEvent> CompetitionEvents { get; set; } = new List<CompetitionEvent>();
}
