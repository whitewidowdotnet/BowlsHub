namespace BowlsLive.Persistence.Identity;

public static class ApplicationRoles
{
    public const string InternalAdmin = nameof(InternalAdmin);
    public const string ClubAdmin = nameof(ClubAdmin);
    public const string ClubMember = nameof(ClubMember);

    public static readonly Guid InternalAdminId = Guid.Parse("d2683966-f6bd-4aa4-bbe2-b27aa3709189");
    public static readonly Guid ClubAdminId = Guid.Parse("93928d58-4764-4ba3-86bc-12ba6c7a0fe1");
    public static readonly Guid ClubMemberId = Guid.Parse("2c6e6708-54f6-4f16-bfbf-b7bd0274c83c");
}
