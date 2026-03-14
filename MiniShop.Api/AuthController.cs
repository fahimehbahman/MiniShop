using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Contracts.Login;
using MiniShop.Application.Interfaces;
using MiniShop.Application.Services;
using MiniShop.Infrastructure.Repositories;
using System.Security.Claims;

namespace MiniShop.Api
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, ILogger<AuthController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.UserName);
            Response.Cookies.Append(
                  "access_token",
                    token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    }
                );
            _logger.LogInformation("{f} Loggedin!", request.UserName);
            return Ok(new { data=request.UserName, message = "Login successful" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var user = await _userService.GetUserById(userId);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                id = user.UserId,
                userName = user.UserName
            });
        }
    }
}
