using Backend.Interfaces;
using Backend.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _authService.LoginAsync(request, cancellationToken));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _authService.RegisterAsync(request, cancellationToken));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _authService.RefreshAsync(request, cancellationToken));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
        {
            await _authService.LogoutAsync(User.Identity!.Name!, cancellationToken);
            return Ok();
        }
    }
}
