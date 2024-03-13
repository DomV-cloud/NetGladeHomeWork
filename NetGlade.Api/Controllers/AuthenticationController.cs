using NetGlade.Contracts.Authentication;
using NetGlade.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using NetGlade.Application.PaginationFilter;

namespace NetGlade.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register", Name = "register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );

            var response = new AuthenticationResponse(
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
                );

            return Ok(response);
        }

        [HttpPost("login", Name = "login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
                );

            return Ok(response);
        }

    }
}
