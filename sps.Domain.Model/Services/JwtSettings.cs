using System;

namespace sps.Domain.Model.Services
{
    /// <summary>
    /// Represents JWT configuration settings from appsettings.json
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret key used for signing tokens
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer of the JWT token
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the audience of the JWT token
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiration time in minutes for JWT tokens
        /// </summary>
        public int ExpirationInMinutes { get; set; } = 60;
    }
}
