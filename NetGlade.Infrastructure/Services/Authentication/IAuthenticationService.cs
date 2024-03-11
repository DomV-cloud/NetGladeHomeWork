using NetGlade.Contracts.Authentication;

namespace NetGlade.Infrastructure.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName, string lastName, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}
