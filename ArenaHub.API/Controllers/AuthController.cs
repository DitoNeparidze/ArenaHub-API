using ArenaHub.API.Constants;
using ArenaHub.API.Dtos.Auth;
using ArenaHub.API.Entities;
using ArenaHub.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, AppRoles.User);

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok("User registered successfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser == null)
                return Unauthorized("Invalid email or password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);

            if (!isPasswordValid)
                return Unauthorized("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(existingUser);

            var token = _tokenService.CreateToken(existingUser, roles);

            return Ok(new AuthResponseDto { Token = token });
        }
    }
}
