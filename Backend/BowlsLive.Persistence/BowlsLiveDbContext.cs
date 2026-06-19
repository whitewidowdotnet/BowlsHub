using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BowlsLive.Domain.Entities;
using BowlsLive.Domain.Enumerations;
using BowlsLive.Persistence.Identity;

namespace BowlsLive.Persistence;

public sealed class BowlsLiveDbContext(DbContextOptions<BowlsLiveDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<ClubMembership> ClubMemberships => Set<ClubMembership>();
    public DbSet<CompetitionDefinition> CompetitionDefinitions => Set<CompetitionDefinition>();
    public DbSet<CompetitionEvent> CompetitionEvents => Set<CompetitionEvent>();
    public DbSet<CompetitionEntry> CompetitionEntries => Set<CompetitionEntry>();
    public DbSet<CompetitionTeam> CompetitionTeams => Set<CompetitionTeam>();
    public DbSet<CompetitionTeamMember> CompetitionTeamMembers => Set<CompetitionTeamMember>();
    public DbSet<CompetitionMatch> CompetitionMatches => Set<CompetitionMatch>();
    public DbSet<CompetitionMatchParticipant> CompetitionMatchParticipants => Set<CompetitionMatchParticipant>();
    public DbSet<CompetitionMatchScorecard> CompetitionMatchScorecards => Set<CompetitionMatchScorecard>();
    public DbSet<CompetitionMatchSegment> CompetitionMatchSegments => Set<CompetitionMatchSegment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FirstName).HasMaxLength(100);
            entity.Property(x => x.LastName).HasMaxLength(100);
            entity.Property(x => x.DisplayName).HasMaxLength(200);
            entity.Property(x => x.BsaRegistrationNumber).HasMaxLength(50);
            entity.Property(x => x.WpbaRegistrationNumber).HasMaxLength(50);
            entity.Property(x => x.Gender).HasConversion<string>().HasMaxLength(32);
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.Property(x => x.Description).HasMaxLength(256);

            entity.HasData(
                new ApplicationRole
                {
                    Id = ApplicationRoles.InternalAdminId,
                    Name = ApplicationRoles.InternalAdmin,
                    NormalizedName = ApplicationRoles.InternalAdmin.ToUpperInvariant(),
                    Description = "Platform administrators with internal operational access.",
                    IsSystemRole = true,
                    ConcurrencyStamp = "3e00ce65-2e77-4322-938b-d4d7b374e35a"
                },
                new ApplicationRole
                {
                    Id = ApplicationRoles.ClubAdminId,
                    Name = ApplicationRoles.ClubAdmin,
                    NormalizedName = ApplicationRoles.ClubAdmin.ToUpperInvariant(),
                    Description = "Club administrators who manage members, competitions, and club setup.",
                    IsSystemRole = true,
                    ConcurrencyStamp = "d6b85462-8946-4d4f-a849-273d81560ac0"
                },
                new ApplicationRole
                {
                    Id = ApplicationRoles.ClubMemberId,
                    Name = ApplicationRoles.ClubMember,
                    NormalizedName = ApplicationRoles.ClubMember.ToUpperInvariant(),
                    Description = "Standard club members who can enter competitions and manage their profile.",
                    IsSystemRole = true,
                    ConcurrencyStamp = "38a459d9-d330-49e5-8f4f-f2897abddc53"
                });
        });

        builder.Entity<Club>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(200);
            entity.Property(x => x.ShortName).HasMaxLength(100);
            entity.Property(x => x.Slug).HasMaxLength(100);
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.PhoneNumber).HasMaxLength(50);
            entity.HasIndex(x => x.Slug).IsUnique();
        });

        builder.Entity<ClubMembership>(entity =>
        {
            entity.Property(x => x.MembershipNumber).HasMaxLength(50);
            entity.Property(x => x.Role).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Notes).HasMaxLength(2000);
            entity.HasIndex(x => new { x.ClubId, x.UserId }).IsUnique();
            entity.HasOne(x => x.Club)
                .WithMany(x => x.Memberships)
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CompetitionDefinition>(entity =>
        {
            entity.Property(x => x.Code).HasMaxLength(20);
            entity.Property(x => x.Name).HasMaxLength(200);
            entity.Property(x => x.Gender).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Format).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Category).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.EntryType).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.ScoringType).HasConversion<string>().HasMaxLength(40);
            entity.Property(x => x.DefaultStageType).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.ChampionOfChampionsTarget).HasMaxLength(200);
            entity.Property(x => x.Notes).HasMaxLength(4000);
            entity.HasIndex(x => x.Code).IsUnique();
            entity.HasData(CompetitionDefinitionSeed.GetAll());
        });

        builder.Entity<CompetitionEvent>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(200);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.EntryFee).HasPrecision(18, 2);
            entity.Property(x => x.Notes).HasMaxLength(4000);
            entity.HasOne(x => x.Club)
                .WithMany(x => x.CompetitionEvents)
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.CompetitionDefinition)
                .WithMany(x => x.CompetitionEvents)
                .HasForeignKey(x => x.CompetitionDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.ClubId, x.CompetitionDefinitionId, x.Season }).IsUnique();
        });

        builder.Entity<CompetitionEntry>(entity =>
        {
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Notes).HasMaxLength(2000);
            entity.HasOne(x => x.CompetitionEvent)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.CompetitionEventId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.ClubMembership)
                .WithMany(x => x.CompetitionEntries)
                .HasForeignKey(x => x.ClubMembershipId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.CompetitionTeam)
                .WithOne(x => x.Entry)
                .HasForeignKey<CompetitionEntry>(x => x.CompetitionTeamId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(x => new { x.CompetitionEventId, x.ClubMembershipId }).IsUnique();
            entity.HasIndex(x => new { x.CompetitionEventId, x.CompetitionTeamId }).IsUnique();
        });

        builder.Entity<CompetitionTeam>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(200);
            entity.HasOne(x => x.CompetitionEvent)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.CompetitionEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CompetitionTeamMember>(entity =>
        {
            entity.Property(x => x.Position).HasMaxLength(50);
            entity.HasOne(x => x.CompetitionTeam)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.CompetitionTeamId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.ClubMembership)
                .WithMany(x => x.TeamMemberships)
                .HasForeignKey(x => x.ClubMembershipId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.CompetitionTeamId, x.ClubMembershipId }).IsUnique();
        });

        builder.Entity<CompetitionMatch>(entity =>
        {
            entity.Property(x => x.StageName).HasMaxLength(100);
            entity.Property(x => x.StageType).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.Venue).HasMaxLength(200);
            entity.Property(x => x.Rink).HasMaxLength(50);
            entity.Property(x => x.Notes).HasMaxLength(2000);
            entity.HasOne(x => x.CompetitionEvent)
                .WithMany(x => x.Matches)
                .HasForeignKey(x => x.CompetitionEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CompetitionMatchParticipant>(entity =>
        {
            entity.Property(x => x.StartingScore).HasPrecision(18, 2);
            entity.Property(x => x.FinalScore).HasPrecision(18, 2);
            entity.Property(x => x.FinalPoints).HasPrecision(18, 2);
            entity.HasOne(x => x.CompetitionMatch)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.CompetitionMatchId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.CompetitionTeam)
                .WithMany(x => x.MatchParticipants)
                .HasForeignKey(x => x.CompetitionTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.CompetitionMatchId, x.Slot }).IsUnique();
        });

        builder.Entity<CompetitionMatchScorecard>(entity =>
        {
            entity.Property(x => x.SegmentType).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.WinningValue).HasPrecision(18, 2);
            entity.Property(x => x.Notes).HasMaxLength(2000);
            entity.HasOne(x => x.CompetitionMatch)
                .WithOne(x => x.Scorecard)
                .HasForeignKey<CompetitionMatchScorecard>(x => x.CompetitionMatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CompetitionMatchSegment>(entity =>
        {
            entity.Property(x => x.Label).HasMaxLength(100);
            entity.Property(x => x.ScoreForParticipantOne).HasPrecision(18, 2);
            entity.Property(x => x.ScoreForParticipantTwo).HasPrecision(18, 2);
            entity.Property(x => x.PointsForParticipantOne).HasPrecision(18, 2);
            entity.Property(x => x.PointsForParticipantTwo).HasPrecision(18, 2);
            entity.Property(x => x.Notes).HasMaxLength(1000);
            entity.HasOne(x => x.CompetitionMatchScorecard)
                .WithMany(x => x.Segments)
                .HasForeignKey(x => x.CompetitionMatchScorecardId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(x => new { x.CompetitionMatchScorecardId, x.Sequence }).IsUnique();
        });
    }
}
