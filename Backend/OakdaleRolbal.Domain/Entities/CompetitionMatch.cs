using OakdaleRolbal.Domain.Common;
using OakdaleRolbal.Domain.Enumerations;

namespace OakdaleRolbal.Domain.Entities;

public sealed class CompetitionMatch : EntityBase
{
    public Guid CompetitionEventId { get; set; }
    public int RoundNumber { get; set; }
    public string StageName { get; set; } = string.Empty;
    public CompetitionStageType StageType { get; set; }
    public CompetitionMatchStatus Status { get; set; } = CompetitionMatchStatus.Pending;
    public DateTime? ScheduledStartUtc { get; set; }
    public DateTime? CompletedOnUtc { get; set; }
    public string? Venue { get; set; }
    public string? Rink { get; set; }
    public int? EndsScheduled { get; set; }
    public int? ExtraEndsPlayed { get; set; }
    public string? Notes { get; set; }

    public CompetitionEvent CompetitionEvent { get; set; } = null!;
    public CompetitionMatchScorecard? Scorecard { get; set; }
    public ICollection<CompetitionMatchParticipant> Participants { get; set; } = new List<CompetitionMatchParticipant>();
}
