using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using sps.API.Controllers.Base;
using sps.BLL;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos;
using sps.Domain.Model.Responses;
using System.Security.Claims;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for authentication endpoints that return JSON responses for NextAuth integration
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtTokenService _jwtTokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="signInManager">The sign-in manager instance.</param>
        /// <param name="jwtSettings">The JWT settings.</param>
        /// <param name="jwtTokenService">The JWT token service.</param>
        public AuthController(
            ILogger<AuthController> logger,
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager,
            IOptions<JwtSettings> jwtSettings,
            IJwtTokenService jwtTokenService) : base(logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Endpoint for user login that returns a JWT token
        /// </summary>
        /// <param name="loginDto">The login credentials</param>
        /// <returns>JWT token and user information</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Find user by email
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("Invalid login attempt", "INVALID_CREDENTIALS"));
                }

                // Check password
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("Invalid login attempt", "INVALID_CREDENTIALS"));
                }

                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                // Generate token using token service
                var token = _jwtTokenService.GenerateJwtToken(user, roles);

                // Return the token and user info
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpiresIn = _jwtSettings.ExpirationInMinutes * 60,
                    UserId = user.Id.ToString(),
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Roles = roles.ToArray()
                };

                return Ok(ServiceResponse<AuthResponseDto>.CreateSuccess(response));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Endpoint to register a new user
        /// </summary>
        /// <param name="registerDto">The registration information</param>
        /// <returns>Result of the registration attempt</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                // Check if email already exists
                var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUser != null)
                {
                    return BadRequest(ServiceResponse<object>.CreateError("Email already registered", "EMAIL_EXISTS"));
                }

                // Create the user
                var user = new IdentityUser<Guid>
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    EmailConfirmed = false // Set to true if you don't need email confirmation
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                
                if (result.Succeeded)
                {
                    // Assign default role
                    await _userManager.AddToRoleAsync(user, "user");
                    
                    // Generate token using token service
                    var roles = new[] { "user" };
                    var token = _jwtTokenService.GenerateJwtToken(user, roles);

                    // Return the token and user info
                    var response = new AuthResponseDto
                    {
                        Token = token,
                        ExpiresIn = _jwtSettings.ExpirationInMinutes * 60,
                        UserId = user.Id.ToString(),
                        Email = user.Email ?? string.Empty,
                        UserName = user.UserName ?? string.Empty,
                        Roles = roles
                    };

                    return Ok(ServiceResponse<AuthResponseDto>.CreateSuccess(response));
                }
                
                // If registration failed, return the errors
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(ServiceResponse<object>.CreateError("Registration failed: " + string.Join(", ", errors), "REGISTRATION_FAILED"));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets the current logged-in user's info
        /// </summary>
        /// <returns>User information</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("User not found", "UNAUTHORIZED"));
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("User not found", "UNAUTHORIZED"));
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userDto = new UserDto
                {
                    Id = user.Id.ToString(),
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Roles = roles.ToArray()
                };

                return Ok(ServiceResponse<UserDto>.CreateSuccess(userDto));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Refreshes the JWT token
        /// </summary>
        /// <returns>New JWT token</returns>
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("Invalid token", "INVALID_TOKEN"));
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(ServiceResponse<object>.CreateError("User not found", "USER_NOT_FOUND"));
                }

                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                // Generate new token using token service
                var token = _jwtTokenService.GenerateJwtToken(user, roles);

                // Return the token
                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpiresIn = _jwtSettings.ExpirationInMinutes * 60,
                    UserId = user.Id.ToString(),
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Roles = roles.ToArray()
                };

                return Ok(ServiceResponse<AuthResponseDto>.CreateSuccess(response));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// NextAuth session endpoint for validating tokens and sessions
        /// </summary>
        /// <returns>User session information for NextAuth</returns>
        [HttpGet("session")]
        public async Task<IActionResult> GetSession()
        {
            try
            {
                // Check if there's an authenticated user
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    // Return empty session for non-authenticated users
                    return Ok(new { });
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Ok(new { });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Ok(new { });
                }

                var roles = await _userManager.GetRolesAsync(user);

                // Return in format that NextAuth expects
                return Ok(new
                {
                    user = new
                    {
                        id = user.Id.ToString(),
                        email = user.Email,
                        name = user.UserName,
                        roles = roles.ToArray()
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error getting session");
                return Ok(new { }); // Return empty session on error
            }
        }

        /// <summary>
        /// NextAuth compatible endpoint for handling CSRF tokens
        /// </summary>
        /// <returns>CSRF token for NextAuth</returns>
        [HttpGet("csrf")]
        [AllowAnonymous]
        public IActionResult GetCsrfToken()
        {
            return Ok(new { csrfToken = Guid.NewGuid().ToString() });
        }
    }
}