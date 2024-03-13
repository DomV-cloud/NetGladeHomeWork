using NetGlade.Domain.Entities;

namespace NetGlade.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
