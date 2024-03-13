using NetGlade.Application.Pagination;
using NetGlade.Contracts.Authentication;
using NetGlade.Domain.Entities;
using NetGlade.Application.PaginationFilter;

namespace NetGlade.Application.Services.Authentication

{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName, string lastName, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}
