namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// Data transfer object for authentication response with JWT token
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// JWT token for authentication
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expiration time in seconds
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's roles
        /// </summary>
        public string[] Roles { get; set; }
    }
}