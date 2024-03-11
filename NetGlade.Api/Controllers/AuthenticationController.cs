using NetGlade.Contracts.Authentication;
using NetGlade.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

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

            var response = new AuthenticationResult(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
                );

            return Ok(response);
        }

        [HttpPost("login", Name = "login")]
        public IActionResult Login(LoginRequest request)
        {
            return Ok(request);
        }
    }
}
