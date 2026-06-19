using BowlsLive.Domain.Common;

namespace BowlsLive.Domain.Entities;

public sealed class CompetitionTeam : EntityBase
{
    public Guid CompetitionEventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Seed { get; set; }
    public Guid? SkipMembershipId { get; set; }

    public CompetitionEvent CompetitionEvent { get; set; } = null!;
    public CompetitionEntry? Entry { get; set; }
    public ICollection<CompetitionTeamMember> Members { get; set; } = new List<CompetitionTeamMember>();
    public ICollection<CompetitionMatchParticipant> MatchParticipants { get; set; } = new List<CompetitionMatchParticipant>();
}
