using BowlsLive.Application.Common.Authentication;

namespace BowlsLive.Application.Interfaces;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(Guid userId, string email, string userName, IEnumerable<string> roles);
}
