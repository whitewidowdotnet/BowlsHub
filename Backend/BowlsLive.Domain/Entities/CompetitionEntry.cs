using BowlsLive.Domain.Common;
using BowlsLive.Domain.Enumerations;

namespace BowlsLive.Domain.Entities;

public sealed class CompetitionEntry : EntityBase
{
    public Guid CompetitionEventId { get; set; }
    public Guid? ClubMembershipId { get; set; }
    public Guid? CompetitionTeamId { get; set; }
    public CompetitionEntryStatus Status { get; set; } = CompetitionEntryStatus.Pending;
    public DateTime AppliedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedOnUtc { get; set; }
    public DateTime? RejectedOnUtc { get; set; }
    public string? Notes { get; set; }

    public CompetitionEvent CompetitionEvent { get; set; } = null!;
    public ClubMembership? ClubMembership { get; set; }
    public CompetitionTeam? CompetitionTeam { get; set; }
}
