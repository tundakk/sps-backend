namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// Data transfer object for user information
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// User ID
        /// </summary>
        public string Id { get; set; }

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