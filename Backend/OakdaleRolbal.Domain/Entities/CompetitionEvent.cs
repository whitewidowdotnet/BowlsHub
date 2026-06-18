using OakdaleRolbal.Domain.Common;
using OakdaleRolbal.Domain.Enumerations;

namespace OakdaleRolbal.Domain.Entities;

public sealed class CompetitionEvent : EntityBase
{
    public Guid ClubId { get; set; }
    public Guid CompetitionDefinitionId { get; set; }
    public int Season { get; set; }
    public string Name { get; set; } = string.Empty;
    public CompetitionStatus Status { get; set; } = CompetitionStatus.Draft;
    public decimal EntryFee { get; set; }
    public DateTime? EntryOpenOnUtc { get; set; }
    public DateTime? EntryCloseOnUtc { get; set; }
    public DateTime? DrawDateUtc { get; set; }
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public string? Notes { get; set; }

    public Club Club { get; set; } = null!;
    public CompetitionDefinition CompetitionDefinition { get; set; } = null!;
    public ICollection<CompetitionEntry> Entries { get; set; } = new List<CompetitionEntry>();
    public ICollection<CompetitionTeam> Teams { get; set; } = new List<CompetitionTeam>();
    public ICollection<CompetitionMatch> Matches { get; set; } = new List<CompetitionMatch>();
}
