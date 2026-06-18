using OakdaleRolbal.Domain.Common;
using OakdaleRolbal.Domain.Enumerations;

namespace OakdaleRolbal.Domain.Entities;

public sealed class ClubMembership : EntityBase
{
    public Guid ClubId { get; set; }
    public Guid UserId { get; set; }
    public string? MembershipNumber { get; set; }
    public ClubMembershipRole Role { get; set; } = ClubMembershipRole.Player;
    public ClubMembershipStatus Status { get; set; } = ClubMembershipStatus.PendingApproval;
    public bool IsPrimaryClub { get; set; }
    public DateTime? InvitedOnUtc { get; set; }
    public DateTime? ApprovedOnUtc { get; set; }
    public DateTime? JoinedOnUtc { get; set; }
    public DateTime? BlockedOnUtc { get; set; }
    public Guid? InvitedByUserId { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public string? Notes { get; set; }

    public Club Club { get; set; } = null!;
    public ICollection<CompetitionEntry> CompetitionEntries { get; set; } = new List<CompetitionEntry>();
    public ICollection<CompetitionTeamMember> TeamMemberships { get; set; } = new List<CompetitionTeamMember>();
}
