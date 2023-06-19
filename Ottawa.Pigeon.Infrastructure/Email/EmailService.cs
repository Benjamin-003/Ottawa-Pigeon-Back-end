using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Options;

using MimeKit;
using MimeKit.Text;

using Ottawa.Pigeon.Application.Interfaces;
using Ottawa.Pigeon.Application.Services.Email;
using Ottawa.Pigeon.Application.Services.Email.Models;

using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Ottawa.Pigeon.Infrastructure.Email
{
    /// <summary>
    /// Service dédié à l'envoi des emails depuis l'application
    /// </summary>
    public class EmailService : IEmailService
    {
        public readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        /// <summary>
        /// Méthode qui envoie un email
        /// </summary>
        /// <param name="request">Un objet EmailDto</param>
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("email", _emailConfig.From));
            email.To.Add(new MailboxAddress("email", request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.Username, _emailConfig.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public ForgotPasswordEmailResource GetForgotPasswordEmailResource(string languageCode)
        {
            var resourceManager = new ResourceManager("Ottawa.Pigeon.Infrastructure.Resources.Email.LocalizationSendForgotPasswordEmailCommand", Assembly.GetExecutingAssembly());
            var cultureInfo = new CultureInfo(languageCode);

            var resource = new ForgotPasswordEmailResource()
            {
                Subject = resourceManager.GetString("Subject", cultureInfo),
                PasswordResetRequest = resourceManager.GetString("PasswordResetRequest", cultureInfo),
                NoActionTaken = resourceManager.GetString("NoActionTaken", cultureInfo),
                PasswordResetAction = resourceManager.GetString("PasswordResetAction", cultureInfo),
                ResetButton = resourceManager.GetString("ResetButton", cultureInfo),
                Cancellation = resourceManager.GetString("Cancellation", cultureInfo),
            };
            return resource;
        }
    }
}