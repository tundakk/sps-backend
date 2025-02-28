using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using sps.Domain.Model.Models;
using sps.BLL.Infrastructure.Interfaces;

namespace sps.API.Controllers
{
    /// <summary>
    /// Controller for handling tablet-specific authentication actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TabletController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly string _jwtSecretKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public TabletController(
            UserManager<IdentityUser<Guid>> userManager, 
            IConfiguration configuration,
            IUserProfileService userProfileService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userProfileService = userProfileService;
            _jwtSecretKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT secret is not configured.");
            _jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            _jwtAudience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured.");
        }

        /// <summary>
        /// Authenticates a user by apartment number and pin code.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>An <see cref="IActionResult"/> containing the JWT token and expiration.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginByPinCode([FromBody] PinCodeLoginModel model)
        {
            var users = await _userManager.Users.ToListAsync();
            var userProfiles = new List<(IdentityUser<Guid> User, UserProfileModel Profile)>();

            foreach (var user in users)
            {
                var profile = await _userProfileService.GetByUserIdAsync(user.Id);
                if (profile != null)
                {
                    userProfiles.Add((user, profile));
                }
            }

            var matchingProfile = userProfiles.FirstOrDefault(up => 
                up.Profile.ApartmentNumber == model.ApartmentNumber && 
                up.Profile.PinCode == model.PinCode);

            if (matchingProfile.User == null)
            {
                return Unauthorized(new { message = "Invalid apartment number or PIN code." });
            }

            var user = matchingProfile.User;
            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = jwtToken,
                expiration = token.ValidTo
            });
        }
    }
}