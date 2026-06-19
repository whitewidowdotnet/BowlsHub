using BowlsLive.Domain.Common;

namespace BowlsLive.Domain.Entities;

public sealed class CompetitionMatchParticipant : EntityBase
{
    public Guid CompetitionMatchId { get; set; }
    public Guid? CompetitionTeamId { get; set; }
    public Guid? ClubMembershipId { get; set; }
    public int Slot { get; set; }
    public decimal StartingScore { get; set; }
    public decimal FinalScore { get; set; }
    public decimal FinalPoints { get; set; }
    public bool IsWinner { get; set; }

    public CompetitionMatch CompetitionMatch { get; set; } = null!;
    public CompetitionTeam? CompetitionTeam { get; set; }
}
