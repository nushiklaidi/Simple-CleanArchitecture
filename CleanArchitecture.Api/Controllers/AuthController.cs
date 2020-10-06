using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Authenticate(User model)
        {
            var user = _authService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorect"});
            }
            return Ok(user);
        }
    }
}
