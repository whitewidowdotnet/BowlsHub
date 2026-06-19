using BowlsLive.Domain.Entities;
using BowlsLive.Domain.Enumerations;

namespace BowlsLive.Persistence;

internal static class CompetitionDefinitionSeed
{
    public static IEnumerable<CompetitionDefinition> GetAll()
    {
        yield return CreateSingles(
            "6b6d4cac-c917-4f3f-968b-b2ffc3c6bb9f",
            "L0101",
            "Ladies Open Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.Open,
            championOfChampionsTarget: "WPBA Champion of Champions - Ladies Open");

        yield return CreateSingles(
            "26024be8-78bf-4117-a77f-f5c700d2eeb6",
            "L0102",
            "Ladies Senior Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.Senior,
            minimumAgeYears: 60,
            championOfChampionsTarget: "WPBA Champion of Champions - Ladies Senior");

        yield return CreateSingles(
            "f2b29233-a719-48eb-bad0-22dfd6cded4d",
            "L0103",
            "Ladies Novice Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.Novice,
            maximumMembershipYears: 3,
            championOfChampionsTarget: "WPBA Champion of Champions - Ladies Novice");

        yield return CreateSingles(
            "c6fc77b3-2771-4f53-b650-c1d8bdb5d9d9",
            "L0104",
            "Ladies Handicap Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.Handicap,
            useHandicap: true);

        yield return CreateSingles(
            "de5a1796-4cb7-4172-9a9d-867c614198b2",
            "L0105",
            "Ladies Consistency Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.Consistency,
            scoringType: CompetitionScoringType.FirstToTargetPoints,
            defaultTargetScore: 100,
            useJackSpotMarking: true,
            notes: "Best four live bowls on each end score points toward a 100-point target.");

        yield return CreateSingles(
            "10ff6ee2-969d-43a2-b982-7dc7090d3fb8",
            "L0106",
            "Ladies 2 Bowls Singles",
            CompetitionGender.Ladies,
            CompetitionCategory.TwoBowls,
            bowlsPerPlayer: 2,
            requiresMarker: false,
            keepJackInPlaceAfterDelivery: true,
            keepJackInPlaceAfterContact: true);

        yield return CreatePairs(
            "834ba881-a6d4-4691-a8aa-58f779d2acfb",
            "L0207",
            "Ladies Nominated Pairs",
            CompetitionGender.Ladies);

        yield return CreateSingles(
            "eb91b2c3-5c8d-4118-8746-cae922af3a8d",
            "M0101",
            "Mens Open Singles",
            CompetitionGender.Mens,
            CompetitionCategory.Open,
            championOfChampionsTarget: "WPBA Champion of Champions - Mens Open");

        yield return CreateSingles(
            "e990da92-ccaf-49fa-8d74-a6480c9109d6",
            "M0102",
            "Mens Senior Singles",
            CompetitionGender.Mens,
            CompetitionCategory.Senior,
            minimumAgeYears: 60,
            championOfChampionsTarget: "WPBA Champion of Champions - Mens Senior");

        yield return CreateSingles(
            "fef517b6-84f6-412f-8c6b-cabf5d399414",
            "M0103",
            "Mens Novice Singles",
            CompetitionGender.Mens,
            CompetitionCategory.Novice,
            maximumMembershipYears: 3,
            championOfChampionsTarget: "WPBA Champion of Champions - Mens Novice");

        yield return CreateSingles(
            "e18fd27c-6dcf-4f42-9f18-9f61cfb786d7",
            "M0104",
            "Mens Handicap Singles",
            CompetitionGender.Mens,
            CompetitionCategory.Handicap,
            useHandicap: true);

        yield return CreateSingles(
            "8db55e84-8668-448c-b2e1-31936abf4b75",
            "M0105",
            "Mens Consistency Singles",
            CompetitionGender.Mens,
            CompetitionCategory.Consistency,
            scoringType: CompetitionScoringType.FirstToTargetPoints,
            defaultTargetScore: 100,
            useJackSpotMarking: true,
            notes: "Best four live bowls on each end score points toward a 100-point target.");

        yield return CreateSingles(
            "e9fdcb7d-b98f-4927-ac43-8a36d3222d29",
            "M0106",
            "Mens 2 Bowls Singles",
            CompetitionGender.Mens,
            CompetitionCategory.TwoBowls,
            bowlsPerPlayer: 2,
            requiresMarker: false,
            keepJackInPlaceAfterDelivery: true,
            keepJackInPlaceAfterContact: true);

        yield return CreatePairs(
            "3989d7c6-eab6-4369-b85c-44de34ac72c9",
            "M0207",
            "Mens Nominated Pairs",
            CompetitionGender.Mens);

        yield return CreatePairs(
            "1fe2ff17-1275-467f-b4de-d5fe4287b2cb",
            "X0201",
            "Mixed Nominated Pairs",
            CompetitionGender.Mixed);
    }

    private static CompetitionDefinition CreateSingles(
        string id,
        string code,
        string name,
        CompetitionGender gender,
        CompetitionCategory category,
        CompetitionScoringType scoringType = CompetitionScoringType.FirstToTargetShots,
        int bowlsPerPlayer = 4,
        int? defaultTargetScore = 21,
        bool requiresMarker = true,
        bool useHandicap = false,
        bool useJackSpotMarking = false,
        bool keepJackInPlaceAfterDelivery = false,
        bool keepJackInPlaceAfterContact = false,
        int? minimumAgeYears = null,
        int? maximumMembershipYears = null,
        string? championOfChampionsTarget = null,
        string? notes = null)
    {
        return new CompetitionDefinition
        {
            Id = Guid.Parse(id),
            Code = code,
            Name = name,
            Gender = gender,
            Format = CompetitionFormat.Singles,
            Category = category,
            EntryType = CompetitionEntryType.Individual,
            ScoringType = scoringType,
            DefaultStageType = CompetitionStageType.Knockout,
            BowlsPerPlayer = bowlsPerPlayer,
            TeamSize = 1,
            DefaultTargetScore = defaultTargetScore,
            RequiresMarker = requiresMarker,
            ReplayBurntEnds = true,
            MinimumEntriesForRoundRobin = 8,
            MinimumAgeYears = minimumAgeYears,
            MaximumMembershipYears = maximumMembershipYears,
            UseHandicap = useHandicap,
            UseJackSpotMarking = useJackSpotMarking,
            KeepJackInPlaceAfterDelivery = keepJackInPlaceAfterDelivery,
            KeepJackInPlaceAfterContact = keepJackInPlaceAfterContact,
            RequiresClubColoursForFinal = true,
            ChampionOfChampionsTarget = championOfChampionsTarget,
            Notes = notes
        };
    }

    private static CompetitionDefinition CreatePairs(
        string id,
        string code,
        string name,
        CompetitionGender gender)
    {
        return new CompetitionDefinition
        {
            Id = Guid.Parse(id),
            Code = code,
            Name = name,
            Gender = gender,
            Format = CompetitionFormat.Pairs,
            Category = CompetitionCategory.Nominated,
            EntryType = CompetitionEntryType.Team,
            ScoringType = CompetitionScoringType.HighestShotsAfterFixedEnds,
            DefaultStageType = CompetitionStageType.Knockout,
            BowlsPerPlayer = 4,
            TeamSize = 2,
            DefaultEnds = 15,
            FinalEnds = 18,
            RequiresMarker = false,
            ReplayBurntEnds = true,
            MinimumEntriesForRoundRobin = 8,
            RequiresClubColoursForFinal = true
        };
    }
}
