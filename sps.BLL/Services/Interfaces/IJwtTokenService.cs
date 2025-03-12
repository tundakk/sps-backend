using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace sps.BLL.Services.Interfaces
{
    /// <summary>
    /// Interface for JWT token service
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for a user with specified roles
        /// </summary>
        /// <param name="user">The user entity</param>
        /// <param name="roles">The roles assigned to the user</param>
        /// <returns>JWT token as string</returns>
        string GenerateJwtToken<TUser>(TUser user, IEnumerable<string> roles) where TUser : IdentityUser<Guid>;
        
        /// <summary>
        /// Validates a JWT token
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>True if the token is valid, otherwise false</returns>
        bool ValidateToken(string token);
        
        /// <summary>
        /// Gets claims from token without validation
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <returns>Claims from token</returns>
        IEnumerable<Claim> GetClaimsFromToken(string token);
    }
}