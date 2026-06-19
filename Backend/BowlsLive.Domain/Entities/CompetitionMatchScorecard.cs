using BowlsLive.Domain.Common;
using BowlsLive.Domain.Enumerations;

namespace BowlsLive.Domain.Entities;

public sealed class CompetitionMatchScorecard : EntityBase
{
    public Guid CompetitionMatchId { get; set; }
    public MatchSegmentType SegmentType { get; set; } = MatchSegmentType.End;
    public decimal? WinningValue { get; set; }
    public string? Notes { get; set; }

    public CompetitionMatch CompetitionMatch { get; set; } = null!;
    public ICollection<CompetitionMatchSegment> Segments { get; set; } = new List<CompetitionMatchSegment>();
}
