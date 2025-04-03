namespace sps.BLL.Email
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using sib_api_v3_sdk.Api;
    using sib_api_v3_sdk.Client;
    using sib_api_v3_sdk.Model;
    using System;
    using System.Collections.Generic;

    // Remove the ambiguous Task import and use fully qualified names where needed

    /// <summary>
    /// Class responsible for sending emails using Brevo (SendinBlue) API.
    /// </summary>
    public class BrevoEmailSender : IExtendedEmailSender<IdentityUser<Guid>>
    {
        private readonly IConfiguration _configuration;
        private readonly TransactionalEmailsApi _emailAPI;
        private readonly ILogger<BrevoEmailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrevoEmailSender"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance to retrieve settings.</param>
        /// <param name="logger">The logger instance for logging information and errors.</param>
        public BrevoEmailSender(IConfiguration configuration, ILogger<BrevoEmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;

            // Retrieve Brevo API settings from appsettings
            var brevoAPIKey = _configuration["BrevoAPI:APIKey"];
            var senderName = _configuration["BrevoAPI:SenderName"];
            var senderEmail = _configuration["BrevoAPI:SenderEmail"];

            // Ensure the API key and sender details are properly configured
            if (string.IsNullOrEmpty(brevoAPIKey))
            {
                throw new InvalidOperationException("Brevo API key is not configured.");
            }

            if (string.IsNullOrEmpty(senderName) || string.IsNullOrEmpty(senderEmail))
            {
                throw new InvalidOperationException("Sender name or email is not configured.");
            }

            // Create a specific configuration instance instead of using static configuration
            var config = new Configuration();
            
            config.ApiKey.Add("api-key", brevoAPIKey);
            
            // Create the API client with explicit configuration
            _emailAPI = new TransactionalEmailsApi(config);
            
            _logger.LogInformation("Brevo email sender initialized successfully");
        }

        /// <summary>
        /// Sends a confirmation link to the specified email.
        /// </summary>
        /// <param name="user">The user to whom the email is sent.</param>
        /// <param name="email">The email address to send the confirmation link to.</param>
        /// <param name="confirmationLink">The confirmation link to be sent.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async System.Threading.Tasks.Task SendConfirmationLinkAsync(IdentityUser<Guid> user, string email, string confirmationLink)
        {
            var subject = "Please confirm your email";
            var message = $"<p>Hello {user.UserName},</p><p>Please confirm your email by clicking <a href=\"{confirmationLink}\">this link</a>.</p>";

            await SendEmailAsync(email, subject, message);
        }

        /// <summary>
        /// Sends a password reset link to the specified email.
        /// </summary>
        /// <param name="user">The user to whom the email is sent.</param>
        /// <param name="email">The email address to send the reset link to.</param>
        /// <param name="resetLink">The password reset link to be sent.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async System.Threading.Tasks.Task SendPasswordResetLinkAsync(IdentityUser<Guid> user, string email, string resetLink)
        {
            var subject = "Password Reset Request";
            var message = $"<p>Hello {user.UserName},</p><p>You can reset your password by clicking <a href=\"{resetLink}\">this link</a>.</p>";

            await SendEmailAsync(email, subject, message);
        }

        /// <summary>
        /// Sends a password reset code to the specified email.
        /// </summary>
        /// <param name="user">The user to whom the email is sent.</param>
        /// <param name="email">The email address to send the reset code to.</param>
        /// <param name="resetCode">The password reset code to be sent.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async System.Threading.Tasks.Task SendPasswordResetCodeAsync(IdentityUser<Guid> user, string email, string resetCode)
        {
            var subject = "Your Password Reset Code";
            var message = $"<p>Hello {user.UserName},</p><p>Your password reset code is: {resetCode}</p>";

            await SendEmailAsync(email, subject, message);
        }

        /// <summary>
        /// Sends a confirmation code to the specified email.
        /// </summary>
        /// <param name="user">The user to whom the email is sent.</param>
        /// <param name="email">The email address to send the confirmation code to.</param>
        /// <param name="confirmationCode">The confirmation code to be sent.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async System.Threading.Tasks.Task SendConfirmationCodeAsync(IdentityUser<Guid> user, string email, string confirmationCode)
        {
            var subject = "Your Email Verification Code";
            var message = $@"<p>Hello {user.UserName},</p>
                            <p>Thanks for registering with our service. To verify your email address, please use the following code:</p>
                            <h2 style='font-size: 24px; padding: 10px; background-color: #f0f0f0; border-radius: 4px; display: inline-block;'>{confirmationCode}</h2>
                            <p>This code will expire in 24 hours.</p>
                            <p>If you didn't request this code, you can safely ignore this email.</p>";

            await SendEmailAsync(email, subject, message);
        }

        // Helper method to send the email using Brevo (SendinBlue)
        private async System.Threading.Tasks.Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var sendSmtpEmail = new SendSmtpEmail
            {
                To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(toEmail) },
                Subject = subject,
                HtmlContent = htmlContent,
                Sender = new SendSmtpEmailSender(_configuration["BrevoAPI:SenderName"], _configuration["BrevoAPI:SenderEmail"]) // Customize sender details
            };

            try
            {
                // Send email using Brevo's API
                var response = await _emailAPI.SendTransacEmailAsync(sendSmtpEmail);
                _logger.LogInformation($"Email sent successfully: {response.MessageId}");
            }
            catch (ApiException ex)
            {
                // Log the exception if the email sending fails
                _logger.LogError($"Error sending email to {toEmail}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch other exceptions
                _logger.LogError($"Unexpected error sending email to {toEmail}: {ex.Message}");
            }
        }
    }
}