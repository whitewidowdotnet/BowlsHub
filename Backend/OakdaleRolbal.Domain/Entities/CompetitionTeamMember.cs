using OakdaleRolbal.Domain.Common;

namespace OakdaleRolbal.Domain.Entities;

public sealed class CompetitionTeamMember : EntityBase
{
    public Guid CompetitionTeamId { get; set; }
    public Guid ClubMembershipId { get; set; }
    public string Position { get; set; } = string.Empty;
    public bool IsSkip { get; set; }

    public CompetitionTeam CompetitionTeam { get; set; } = null!;
    public ClubMembership ClubMembership { get; set; } = null!;
}
