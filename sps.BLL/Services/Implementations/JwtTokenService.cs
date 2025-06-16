using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace sps.BLL.Services.Implementations
{
    /// <summary>
    /// Service responsible for JWT token operations
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Initializes a new instance of the JwtTokenService
        /// </summary>
        /// <param name="jwtSettings">JWT configuration settings</param>
        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Generates a JWT token for a user with specified roles
        /// </summary>
        /// <param name="user">The user entity</param>
        /// <param name="roles">The roles assigned to the user</param>
        /// <returns>JWT token as string</returns>
        public string GenerateJwtToken<TUser>(TUser user, IEnumerable<string> roles)
            where TUser : IdentityUser<Guid>
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT token
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>True if the token is valid, otherwise false</returns>
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets claims from token without validation
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <returns>Claims from token</returns>
        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
    }
}