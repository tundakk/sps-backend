using Microsoft.AspNetCore.Identity;

namespace sps.BLL.Email
{
    /// <summary>
    /// Extended email sender interface that adds support for sending confirmation codes
    /// </summary>
    public interface IExtendedEmailSender<TUser> : IEmailSender<TUser> where TUser : class
    {
        /// <summary>
        /// Sends a confirmation code to the specified email.
        /// </summary>
        /// <param name="user">The user to whom the email is sent.</param>
        /// <param name="email">The email address to send the confirmation code to.</param>
        /// <param name="confirmationCode">The confirmation code to be sent.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendConfirmationCodeAsync(TUser user, string email, string confirmationCode);
    }
}