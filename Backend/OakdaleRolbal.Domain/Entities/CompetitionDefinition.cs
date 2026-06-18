using OakdaleRolbal.Domain.Common;
using OakdaleRolbal.Domain.Enumerations;

namespace OakdaleRolbal.Domain.Entities;

public sealed class CompetitionDefinition : EntityBase
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public CompetitionGender Gender { get; set; }
    public CompetitionFormat Format { get; set; }
    public CompetitionCategory Category { get; set; }
    public CompetitionEntryType EntryType { get; set; }
    public CompetitionScoringType ScoringType { get; set; }
    public CompetitionStageType DefaultStageType { get; set; }
    public int BowlsPerPlayer { get; set; }
    public int TeamSize { get; set; }
    public int? DefaultTargetScore { get; set; }
    public int? DefaultEnds { get; set; }
    public int? FinalEnds { get; set; }
    public bool RequiresMarker { get; set; }
    public bool ReplayBurntEnds { get; set; }
    public int MinimumEntriesForRoundRobin { get; set; } = 8;
    public int? MinimumAgeYears { get; set; }
    public int? MaximumMembershipYears { get; set; }
    public bool UseHandicap { get; set; }
    public bool UseJackSpotMarking { get; set; }
    public bool KeepJackInPlaceAfterDelivery { get; set; }
    public bool KeepJackInPlaceAfterContact { get; set; }
    public bool RequiresClubColoursForFinal { get; set; }
    public string? ChampionOfChampionsTarget { get; set; }
    public string? Notes { get; set; }

    public ICollection<CompetitionEvent> CompetitionEvents { get; set; } = new List<CompetitionEvent>();
}
