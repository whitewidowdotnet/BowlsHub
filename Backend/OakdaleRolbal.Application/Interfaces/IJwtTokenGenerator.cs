using OakdaleRolbal.Application.Common.Authentication;

namespace OakdaleRolbal.Application.Interfaces;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(Guid userId, string email, string userName);
}
