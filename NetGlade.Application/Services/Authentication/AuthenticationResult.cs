using NetGlade.Domain.Entities;

namespace NetGlade.Application.Services.Authentication
{
    public record AuthenticationResult
    (
        User User,
        string Token
    );
}
