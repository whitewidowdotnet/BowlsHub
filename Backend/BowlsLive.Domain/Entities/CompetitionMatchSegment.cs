using BowlsLive.Domain.Common;

namespace BowlsLive.Domain.Entities;

public sealed class CompetitionMatchSegment : EntityBase
{
    public Guid CompetitionMatchScorecardId { get; set; }
    public int Sequence { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal ScoreForParticipantOne { get; set; }
    public decimal ScoreForParticipantTwo { get; set; }
    public decimal PointsForParticipantOne { get; set; }
    public decimal PointsForParticipantTwo { get; set; }
    public bool IsReplay { get; set; }
    public string? Notes { get; set; }

    public CompetitionMatchScorecard CompetitionMatchScorecard { get; set; } = null!;
}
