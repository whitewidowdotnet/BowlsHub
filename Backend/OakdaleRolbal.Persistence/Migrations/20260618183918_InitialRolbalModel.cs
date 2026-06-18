using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OakdaleRolbal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialRolbalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsSystemRole = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    BsaRegistrationNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WpbaRegistrationNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegisteredBowlerSinceUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Format = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Category = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    EntryType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ScoringType = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DefaultStageType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    BowlsPerPlayer = table.Column<int>(type: "integer", nullable: false),
                    TeamSize = table.Column<int>(type: "integer", nullable: false),
                    DefaultTargetScore = table.Column<int>(type: "integer", nullable: true),
                    DefaultEnds = table.Column<int>(type: "integer", nullable: true),
                    FinalEnds = table.Column<int>(type: "integer", nullable: true),
                    RequiresMarker = table.Column<bool>(type: "boolean", nullable: false),
                    ReplayBurntEnds = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumEntriesForRoundRobin = table.Column<int>(type: "integer", nullable: false),
                    MinimumAgeYears = table.Column<int>(type: "integer", nullable: true),
                    MaximumMembershipYears = table.Column<int>(type: "integer", nullable: true),
                    UseHandicap = table.Column<bool>(type: "boolean", nullable: false),
                    UseJackSpotMarking = table.Column<bool>(type: "boolean", nullable: false),
                    KeepJackInPlaceAfterDelivery = table.Column<bool>(type: "boolean", nullable: false),
                    KeepJackInPlaceAfterContact = table.Column<bool>(type: "boolean", nullable: false),
                    RequiresClubColoursForFinal = table.Column<bool>(type: "boolean", nullable: false),
                    ChampionOfChampionsTarget = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsPrimaryClub = table.Column<bool>(type: "boolean", nullable: false),
                    InvitedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    JoinedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BlockedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InvitedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubMemberships_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Season = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    EntryFee = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EntryOpenOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EntryCloseOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DrawDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionEvents_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionEvents_CompetitionDefinitions_CompetitionDefinit~",
                        column: x => x.CompetitionDefinitionId,
                        principalTable: "CompetitionDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionMatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoundNumber = table.Column<int>(type: "integer", nullable: false),
                    StageName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StageType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ScheduledStartUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Venue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Rink = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EndsScheduled = table.Column<int>(type: "integer", nullable: true),
                    ExtraEndsPlayed = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionMatches_CompetitionEvents_CompetitionEventId",
                        column: x => x.CompetitionEventId,
                        principalTable: "CompetitionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Seed = table.Column<int>(type: "integer", nullable: true),
                    SkipMembershipId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionTeams_CompetitionEvents_CompetitionEventId",
                        column: x => x.CompetitionEventId,
                        principalTable: "CompetitionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionMatchScorecards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionMatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    SegmentType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    WinningValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionMatchScorecards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionMatchScorecards_CompetitionMatches_CompetitionMa~",
                        column: x => x.CompetitionMatchId,
                        principalTable: "CompetitionMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubMembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompetitionTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    AppliedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RejectedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionEntries_ClubMemberships_ClubMembershipId",
                        column: x => x.ClubMembershipId,
                        principalTable: "ClubMemberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionEntries_CompetitionEvents_CompetitionEventId",
                        column: x => x.CompetitionEventId,
                        principalTable: "CompetitionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEntries_CompetitionTeams_CompetitionTeamId",
                        column: x => x.CompetitionTeamId,
                        principalTable: "CompetitionTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionMatchParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionMatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClubMembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    Slot = table.Column<int>(type: "integer", nullable: false),
                    StartingScore = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalScore = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalPoints = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsWinner = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionMatchParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionMatchParticipants_CompetitionMatches_Competition~",
                        column: x => x.CompetitionMatchId,
                        principalTable: "CompetitionMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionMatchParticipants_CompetitionTeams_CompetitionTe~",
                        column: x => x.CompetitionTeamId,
                        principalTable: "CompetitionTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsSkip = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionTeamMembers_ClubMemberships_ClubMembershipId",
                        column: x => x.ClubMembershipId,
                        principalTable: "ClubMemberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionTeamMembers_CompetitionTeams_CompetitionTeamId",
                        column: x => x.CompetitionTeamId,
                        principalTable: "CompetitionTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionMatchSegments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionMatchScorecardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ScoreForParticipantOne = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ScoreForParticipantTwo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsForParticipantOne = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsForParticipantTwo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsReplay = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionMatchSegments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionMatchSegments_CompetitionMatchScorecards_Competi~",
                        column: x => x.CompetitionMatchScorecardId,
                        principalTable: "CompetitionMatchScorecards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "IsSystemRole", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2c6e6708-54f6-4f16-bfbf-b7bd0274c83c"), "38a459d9-d330-49e5-8f4f-f2897abddc53", "Standard club members who can enter competitions and manage their profile.", true, "ClubMember", "CLUBMEMBER" },
                    { new Guid("93928d58-4764-4ba3-86bc-12ba6c7a0fe1"), "d6b85462-8946-4d4f-a849-273d81560ac0", "Club administrators who manage members, competitions, and club setup.", true, "ClubAdmin", "CLUBADMIN" },
                    { new Guid("d2683966-f6bd-4aa4-bbe2-b27aa3709189"), "3e00ce65-2e77-4322-938b-d4d7b374e35a", "Platform administrators with internal operational access.", true, "InternalAdmin", "INTERNALADMIN" }
                });

            migrationBuilder.InsertData(
                table: "CompetitionDefinitions",
                columns: new[] { "Id", "BowlsPerPlayer", "Category", "ChampionOfChampionsTarget", "Code", "DefaultEnds", "DefaultStageType", "DefaultTargetScore", "EntryType", "FinalEnds", "Format", "Gender", "KeepJackInPlaceAfterContact", "KeepJackInPlaceAfterDelivery", "MaximumMembershipYears", "MinimumAgeYears", "MinimumEntriesForRoundRobin", "Name", "Notes", "ReplayBurntEnds", "RequiresClubColoursForFinal", "RequiresMarker", "ScoringType", "TeamSize", "UseHandicap", "UseJackSpotMarking" },
                values: new object[,]
                {
                    { new Guid("10ff6ee2-969d-43a2-b982-7dc7090d3fb8"), 2, "TwoBowls", null, "L0106", null, "Knockout", 21, "Individual", null, "Singles", "Ladies", true, true, null, null, 8, "Ladies 2 Bowls Singles", null, true, true, false, "FirstToTargetShots", 1, false, false },
                    { new Guid("1fe2ff17-1275-467f-b4de-d5fe4287b2cb"), 4, "Nominated", null, "X0201", 15, "Knockout", null, "Team", 18, "Pairs", "Mixed", false, false, null, null, 8, "Mixed Nominated Pairs", null, true, true, false, "HighestShotsAfterFixedEnds", 2, false, false },
                    { new Guid("26024be8-78bf-4117-a77f-f5c700d2eeb6"), 4, "Senior", "WPBA Champion of Champions - Ladies Senior", "L0102", null, "Knockout", 21, "Individual", null, "Singles", "Ladies", false, false, null, 60, 8, "Ladies Senior Singles", null, true, true, true, "FirstToTargetShots", 1, false, false },
                    { new Guid("3989d7c6-eab6-4369-b85c-44de34ac72c9"), 4, "Nominated", null, "M0207", 15, "Knockout", null, "Team", 18, "Pairs", "Mens", false, false, null, null, 8, "Mens Nominated Pairs", null, true, true, false, "HighestShotsAfterFixedEnds", 2, false, false },
                    { new Guid("6b6d4cac-c917-4f3f-968b-b2ffc3c6bb9f"), 4, "Open", "WPBA Champion of Champions - Ladies Open", "L0101", null, "Knockout", 21, "Individual", null, "Singles", "Ladies", false, false, null, null, 8, "Ladies Open Singles", null, true, true, true, "FirstToTargetShots", 1, false, false },
                    { new Guid("834ba881-a6d4-4691-a8aa-58f779d2acfb"), 4, "Nominated", null, "L0207", 15, "Knockout", null, "Team", 18, "Pairs", "Ladies", false, false, null, null, 8, "Ladies Nominated Pairs", null, true, true, false, "HighestShotsAfterFixedEnds", 2, false, false },
                    { new Guid("8db55e84-8668-448c-b2e1-31936abf4b75"), 4, "Consistency", null, "M0105", null, "Knockout", 100, "Individual", null, "Singles", "Mens", false, false, null, null, 8, "Mens Consistency Singles", "Best four live bowls on each end score points toward a 100-point target.", true, true, true, "FirstToTargetPoints", 1, false, true },
                    { new Guid("c6fc77b3-2771-4f53-b650-c1d8bdb5d9d9"), 4, "Handicap", null, "L0104", null, "Knockout", 21, "Individual", null, "Singles", "Ladies", false, false, null, null, 8, "Ladies Handicap Singles", null, true, true, true, "FirstToTargetShots", 1, true, false },
                    { new Guid("de5a1796-4cb7-4172-9a9d-867c614198b2"), 4, "Consistency", null, "L0105", null, "Knockout", 100, "Individual", null, "Singles", "Ladies", false, false, null, null, 8, "Ladies Consistency Singles", "Best four live bowls on each end score points toward a 100-point target.", true, true, true, "FirstToTargetPoints", 1, false, true },
                    { new Guid("e18fd27c-6dcf-4f42-9f18-9f61cfb786d7"), 4, "Handicap", null, "M0104", null, "Knockout", 21, "Individual", null, "Singles", "Mens", false, false, null, null, 8, "Mens Handicap Singles", null, true, true, true, "FirstToTargetShots", 1, true, false },
                    { new Guid("e990da92-ccaf-49fa-8d74-a6480c9109d6"), 4, "Senior", "WPBA Champion of Champions - Mens Senior", "M0102", null, "Knockout", 21, "Individual", null, "Singles", "Mens", false, false, null, 60, 8, "Mens Senior Singles", null, true, true, true, "FirstToTargetShots", 1, false, false },
                    { new Guid("e9fdcb7d-b98f-4927-ac43-8a36d3222d29"), 2, "TwoBowls", null, "M0106", null, "Knockout", 21, "Individual", null, "Singles", "Mens", true, true, null, null, 8, "Mens 2 Bowls Singles", null, true, true, false, "FirstToTargetShots", 1, false, false },
                    { new Guid("eb91b2c3-5c8d-4118-8746-cae922af3a8d"), 4, "Open", "WPBA Champion of Champions - Mens Open", "M0101", null, "Knockout", 21, "Individual", null, "Singles", "Mens", false, false, null, null, 8, "Mens Open Singles", null, true, true, true, "FirstToTargetShots", 1, false, false },
                    { new Guid("f2b29233-a719-48eb-bad0-22dfd6cded4d"), 4, "Novice", "WPBA Champion of Champions - Ladies Novice", "L0103", null, "Knockout", 21, "Individual", null, "Singles", "Ladies", false, false, 3, null, 8, "Ladies Novice Singles", null, true, true, true, "FirstToTargetShots", 1, false, false },
                    { new Guid("fef517b6-84f6-412f-8c6b-cabf5d399414"), 4, "Novice", "WPBA Champion of Champions - Mens Novice", "M0103", null, "Knockout", 21, "Individual", null, "Singles", "Mens", false, false, 3, null, 8, "Mens Novice Singles", null, true, true, true, "FirstToTargetShots", 1, false, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberships_ClubId_UserId",
                table: "ClubMemberships",
                columns: new[] { "ClubId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_Slug",
                table: "Clubs",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionDefinitions_Code",
                table: "CompetitionDefinitions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEntries_ClubMembershipId",
                table: "CompetitionEntries",
                column: "ClubMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEntries_CompetitionEventId_ClubMembershipId",
                table: "CompetitionEntries",
                columns: new[] { "CompetitionEventId", "ClubMembershipId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEntries_CompetitionEventId_CompetitionTeamId",
                table: "CompetitionEntries",
                columns: new[] { "CompetitionEventId", "CompetitionTeamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEntries_CompetitionTeamId",
                table: "CompetitionEntries",
                column: "CompetitionTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEvents_ClubId_CompetitionDefinitionId_Season",
                table: "CompetitionEvents",
                columns: new[] { "ClubId", "CompetitionDefinitionId", "Season" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEvents_CompetitionDefinitionId",
                table: "CompetitionEvents",
                column: "CompetitionDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMatches_CompetitionEventId",
                table: "CompetitionMatches",
                column: "CompetitionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMatchParticipants_CompetitionMatchId_Slot",
                table: "CompetitionMatchParticipants",
                columns: new[] { "CompetitionMatchId", "Slot" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMatchParticipants_CompetitionTeamId",
                table: "CompetitionMatchParticipants",
                column: "CompetitionTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMatchScorecards_CompetitionMatchId",
                table: "CompetitionMatchScorecards",
                column: "CompetitionMatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMatchSegments_CompetitionMatchScorecardId_Sequen~",
                table: "CompetitionMatchSegments",
                columns: new[] { "CompetitionMatchScorecardId", "Sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTeamMembers_ClubMembershipId",
                table: "CompetitionTeamMembers",
                column: "ClubMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTeamMembers_CompetitionTeamId_ClubMembershipId",
                table: "CompetitionTeamMembers",
                columns: new[] { "CompetitionTeamId", "ClubMembershipId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTeams_CompetitionEventId",
                table: "CompetitionTeams",
                column: "CompetitionEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompetitionEntries");

            migrationBuilder.DropTable(
                name: "CompetitionMatchParticipants");

            migrationBuilder.DropTable(
                name: "CompetitionMatchSegments");

            migrationBuilder.DropTable(
                name: "CompetitionTeamMembers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CompetitionMatchScorecards");

            migrationBuilder.DropTable(
                name: "ClubMemberships");

            migrationBuilder.DropTable(
                name: "CompetitionTeams");

            migrationBuilder.DropTable(
                name: "CompetitionMatches");

            migrationBuilder.DropTable(
                name: "CompetitionEvents");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "CompetitionDefinitions");
        }
    }
}
